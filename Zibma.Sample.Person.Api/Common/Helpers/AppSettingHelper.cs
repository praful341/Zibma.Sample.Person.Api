using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Zibma.Sample.Person.Api.Common.Models;

namespace Zibma.Sample.Person.Api.Common.Helpers
{
    public class AppSettingHelper
    {
        private static AppSettings appSettings;

        public static AppSettings GetSettings()
        {
            if (appSettings == null)
            {
                appSettings = ReadSettingsfromFile();
            }

            return appSettings;
        }

        public static void RefreshSettings()
        {
            appSettings = ReadSettingsfromFile();
        }

        public static string GetJson()
        {
            string directoryName = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
            string path = Path.Combine(directoryName, "AppSettingPath.txt");
            if (File.Exists(path))
            {
                path = File.ReadAllText(path).Trim();
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    path = "";
                }
            }
            else
            {
                path = "";
            }

            if (string.IsNullOrEmpty(path))
            {
                path = Path.Combine(directoryName, "appsettings.json");
            }

            return File.ReadAllText(path);
        }

        private static AppSettings ReadSettingsfromFile()
        {
            string json = RemoveComments(GetJson());
            return JsonSerializer.Deserialize<AppSettings>(json);
        }

        private static string RemoveComments(string input)
        {
            input = Regex.Replace(input, "^\\s*//.*$", "", RegexOptions.Multiline);
            input = Regex.Replace(input, "^\\s*/\\*(\\s|\\S)*?\\*/\\s*$", "", RegexOptions.Multiline);
            return input;
        }


        public static Stream GetStream()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(GetJson());
            return new MemoryStream(bytes);
        }

    }
}
