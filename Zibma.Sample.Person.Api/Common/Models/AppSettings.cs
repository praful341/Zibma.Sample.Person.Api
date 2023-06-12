namespace Zibma.Sample.Person.Api.Common.Models
{
    public class AppSettings
    {
        public string AllowedHosts { get; set; } = "*";


        public CSSettings ConnectionStrings { get; set; }

        public bool IsDevMode { get; set; }

        public MSProjectSettings MSProject { get; set; }

        public TokenSettings AccessToken { get; set; }

        public TokenSettings RefreshToken { get; set; }

        public RabbitMqSettings RabbitMq { get; set; }

        public FTPSettings FTP { get; set; }

        public SerilogSettings Serilog { get; set; }
    }

    public class CSSettings
    {
        public string Main { get; set; }
    }

    public class MSProjectSettings
    {
        public string APIKey { get; set; }

        public string LoginUrl { get; set; }

        public string UserUrl { get; set; }
    }

    public class TokenSettings
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public string SecretKey { get; set; }

        public int TokenExpireInMinutes { get; set; }
    }


    public class RabbitMqSettings
    {
        public string HostUrl { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class FTPSettings
    {
        public string Host { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string DownloadURL { get; set; }
    }


    public class SerilogSettings
    {
        public SerilogMinimumLevel MinimumLevel { get; set; }

        public List<string> Enrich { get; set; }

        public List<SerilogWriteTo> WriteTo { get; set; }
    }

    public class SerilogMinimumLevel
    {
        public string Default { get; set; } = "Information";


        public SerilogMinimumLevelOverride Override { get; set; }
    }

    public class SerilogMinimumLevelOverride
    {
        public string Microsoft { get; set; } = "Warning";


        public string System { get; set; } = "Warning";

    }

    public class SerilogWriteTo
    {
        public string Name { get; set; }

        public SerilogWriteToArgs Args { get; set; }
    }

    public class SerilogWriteToArgs
    {
        public string path { get; set; }

        public string formatter { get; set; }

        public string outputTemplate { get; set; }

        public string serverUrl { get; set; }

        public string apiKey { get; set; }
    }


}
