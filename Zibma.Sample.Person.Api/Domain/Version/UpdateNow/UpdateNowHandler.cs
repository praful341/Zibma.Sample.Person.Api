using BLL;
using MassTransit;
using MediatR;
using System.Net;
using Zibma.Sample.Person.Api.Common.Enum;
using Zibma.Sample.Person.Api.Common.Helpers;
using Zibma.Sample.Person.Api.Common.Models;
using Zibma.Sample.Person.Api.Domain.Version.UpdateNow.DB;

namespace Zibma.Sample.Person.Api.Domain.Version.UpdateNow
{
    public class UpdateNowHandler : IRequestHandler<UpdateNowCommand, UpdateNowResponseModel>
    {
        private readonly DateTime LatestUpdate = new(2023, 05, 30, (12 + 04), 30, 00);
        private readonly int LatestVersion = 1;

        private IPublishEndpoint _publishEndpoint { get; }
        private ILogger<UpdateNowHandler> _logger { get; }
        public UpdateNowHandler(ILogger<UpdateNowHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<UpdateNowResponseModel> Handle(UpdateNowCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await VersionUpdate();
                if (response.Success)
                {
                    return new UpdateNowResponseModel()
                    {
                        LatestUpdate = LatestUpdate,
                        LatestVersion = LatestVersion,
                        StatusCode = HttpStatusCode.OK,
                        ResponseMessage = "Version Update Successfully."
                    };
                }
                else
                {
                    _logger.LogError(response.Message + " at {Time} in {Handler}", DateTime.Now, nameof(UpdateNowHandler));
                    return new UpdateNowResponseModel() { StatusCode = HttpStatusCode.InternalServerError, LatestUpdate = LatestUpdate, Errors = new List<string>() { response.Message } };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occur at {Time} in {Handler}", DateTime.Now, nameof(UpdateNowHandler));
                return new UpdateNowResponseModel() { StatusCode = HttpStatusCode.InternalServerError, LatestUpdate = LatestUpdate, Errors = new List<string>() { ex.Message } };
            }
        }

        private async Task<BasicResponse> VersionUpdate()
        {
            var response = await UpdateSchoolDB();
            if (!response.Success)
                return response;

            return new BasicResponse() { Success = true };
        }

        private async Task<BasicResponse> UpdateSchoolDB()
        {
            for (int j = 0; ; j++)
            {
                int CurrntVersion = await GetDBVersion();

                if (CurrntVersion >= LatestVersion)
                    break;

                switch (CurrntVersion)
                {
                    #region 0 T0 100

                    case 0:
                        #region 0 to 1
                        if (!await CreateTable_1())
                        {
                            return new BasicResponse() { Success = false, Message = "Error at CreateTable 1" };
                        }
                        if (!await UpdateDBVersion(1))
                        {
                            return new BasicResponse() { Success = false, Message = "Error at UpdateDB 1" };
                        }
                        #endregion
                        break;

                        #endregion
                }
            }
            return new BasicResponse() { Success = true };
        }

        private async Task<int> GetDBVersion()
        {
            return Convert.ToInt32(await NameValueHelper.GetNameValue(eNameValue.DBVersion));
        }

        private async Task<bool> UpdateDBVersion(int Version)
        {
            try
            {
                await NameValueHelper.SetNameValue(eNameValue.DBVersion, Version.ToString());
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occur while Version: Update DBVersion_" + Version + " Fail at {Time} in {Handler}", DateTime.Now, nameof(UpdateNowHandler));
                return false;
            }
        }

        #region Version 1 to 100

        #region Version 1 (21-04-2023)
        private async Task<bool> CreateTable_1()
        {
            try
            {
                string sql = "select top 1 * from NameValue";
                try { await QueryBLL.ExeNonQuery(sql, false, null); }
                catch { await QueryBLL.ExeNonQuery(V_1.GetDBTableCreationScript_NameValue(), false, null); }


                sql = "select top 1 * from Person";
                try { await QueryBLL.ExeNonQuery(sql, false, null); }
                catch { await QueryBLL.ExeNonQuery(V_1.GetDBTableCreationScript_Person(), false, null); }

                sql = "select top 1 * from Student";
                try { await QueryBLL.ExeNonQuery(sql, false, null); }
                catch { await QueryBLL.ExeNonQuery(V_1.GetDBTableCreationScript_Student(), false, null); }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occur while Version: Create DBVersion_1 Fail at {Time} in {Handler}", DateTime.Now, nameof(UpdateNowHandler));
                return false;
            }

            return true;
        }
        #endregion

        #endregion

        private static async Task<bool> AddColumnInTable(string TableName, string ColumnName, string DataType, object DefaultValue)
        {
            if (await IsTableContainColumn(TableName, ColumnName))
                return true;

            string Query = "ALTER TABLE " + TableName + " ADD " + ColumnName + " " + DataType;
            await QueryBLL.ExeNonQuery(Query, false, null);
            if (DefaultValue != null)
            {
                if (DataType.ToUpper() == "BIT")
                    DefaultValue = "'" + DefaultValue.ToString().Replace("'", "''") + "'";
                else if (DataType.ToUpper() == "DATETIME")
                    DefaultValue = "'" + Convert.ToDateTime(DefaultValue).ToString("MM-dd-yyyy hh:mm:ss tt") + "'";
                else if (DataType.ToUpper().Contains("VARCHAR"))
                    DefaultValue = "N'" + DefaultValue.ToString().Replace("'", "''") + "'";

                Query = "UPDATE " + TableName + " SET " + ColumnName + " = " + DefaultValue;
                await QueryBLL.ExeNonQuery(Query, false, null);
            }
            return true;
        }

        private static async Task<bool> IsTableContainColumn(string TableName, string ColumnName)
        {
            var lstColumns = await DAL.BusinessObject.GetColums(TableName, DBU.GetConnectionString());

            return lstColumns.Contains(ColumnName);
        }

        private static async Task<bool> IsTableContainConstraint(string ConstraintName)
        {
            string sql = "select count(1) from sys.indexes Where name = '" + ConstraintName + "'";
            try
            {
                if (await QueryBLL.ExeScalar(sql) > 0)
                    return true;

                return false;
            }
            catch { return false; }
        }

    }
}
