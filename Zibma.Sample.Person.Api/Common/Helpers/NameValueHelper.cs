using BLL;
using BOL;
using Microsoft.IdentityModel.Tokens;
using Zibma.Sample.Person.Api.Common.Enum;
using Zibma.Sample.Person.Api.Domain.Version.UpdateNow.DB;

namespace Zibma.Sample.Person.Api.Common.Helpers
{
    public static class NameValueHelper
    {
        private static string GetDefaultValue(eNameValue NameValue)
        {
            switch (NameValue)
            {
                case eNameValue.DBVersion:
                    return "0";

                default:
                    return string.Empty;
            }
        }

        public static async Task<string> GetNameValue(eNameValue VersionValue)
        {
            #region Create Table if Not Exists
            try { await NameValueBLL.SelectList(new NameValue()); }
            catch
            {
                string sql = "select top 1 * from NameValue";
                try { await QueryBLL.ExeNonQuery(sql, false, null); }
                catch { await QueryBLL.ExeNonQuery(V_1.GetDBTableCreationScript_NameValue(), false, null); }
            }
            #endregion

            var lstNameValue = await NameValueBLL.SelectList(new NameValue()
            {
                Name = VersionValue.ToString(),
            });

            if (lstNameValue.Count() > 0)
            {
                if (lstNameValue.First().Value.IsNullOrEmpty())
                    return GetDefaultValue(VersionValue);
                else
                    return lstNameValue.First().Value.ToString();
            }
            else
            {
                string Value = GetDefaultValue(VersionValue);
                await NameValueBLL.Insert(new NameValue()
                {
                    Name = VersionValue.ToString(),
                    Value = Value,
                });
                return Value;
            }
        }

        public static async Task<bool> SetNameValue(eNameValue VersionValue, string Value)
        {
            try
            {
                var lstNameValue = await NameValueBLL.SelectList(new NameValue() { Name = VersionValue.ToString() });
                if (lstNameValue.Count() > 0)
                {
                    await NameValueBLL.Update(new NameValue()
                    {
                        NameValueId = lstNameValue.First().NameValueId,
                        Name = VersionValue.ToString(),
                        Value = Value,
                    });
                }
                else
                {
                    await NameValueBLL.Insert(new NameValue() { Name = VersionValue.ToString(), Value = Value });
                }
                return true;
            }
            catch { return false; }
        }
    }
}
