using DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class BusinessController<T> where T : IModel
    {
        #region Insert

        public static async Task Insert(T objModel)
        {
            await DAL.DataController<T>.Insert(objModel, DBU.GetConnectionString());
        }

        public static async Task<int> InsertId(T objModel)
        {
            return await DAL.DataController<T>.InsertId(objModel, DBU.GetConnectionString());
        }

        #endregion

        #region Update

        public static async Task<int> Update(T objModel)
        {
            return await DAL.DataController<T>.Update(objModel, DBU.GetConnectionString());
        }

        #endregion

        #region Delete

        public static async Task<int> Delete(T objModel)
        {
            return await DAL.DataController<T>.Delete(objModel, DBU.GetConnectionString());
        }

        #endregion

        #region Select Count

        public static async Task<int> SelectCount(T objModel)
        {
            return await DAL.DataController<T>.SelectCount(objModel, DBU.GetConnectionString());
        }

        #endregion

        #region Select List

        public static async Task<IEnumerable<T>> SelectList(T objModel)
        {
            return await DAL.DataController<T>.SelectList(objModel, DBU.GetConnectionString());
        }

        #endregion

        #region Custom Select

        public static async Task<IEnumerable<U>> ExeQuery<U>(T objModel, eQuery query)
        {
            return await DataController<T>.ExeQuery<U>(objModel, new Queries().GetQuery(query), false, DBU.GetConnectionString());
        }

        public static async Task<IEnumerable<U>> ExeQuery<U>(string sql, bool isSrtoredProc) where U : IModel
        {
            return await DataController<U>.ExeQuery(sql, isSrtoredProc, DBU.GetConnectionString());
        }

        public static async Task<int> ExeNonQuery(T objModel, eQuery query)
        {
            return await DataController<T>.ExeNonQuery(objModel, new Queries().GetQuery(query), false, DBU.GetConnectionString());
        }

        public static async Task<int> ExeNonQuery(string commandText, bool isStoredProc, Dictionary<string, object> param)
        {
            return await DataController<T>.ExeNonQuery(commandText, isStoredProc, DBU.GetConnectionString());
        }

        public static async Task<int> ExeScalar(T objModel, eQuery query)
        {
            return await DataController<T>.ExeScalar<int>(objModel, new Queries().GetQuery(query), false, DBU.GetConnectionString());
        }

        public static async Task<int> ExeScalar(string sql)
        {
            return await DataController<T>.ExeScalar<int>(sql, null, false, DBU.GetConnectionString());
        }

        #endregion
    }

    public class DBU
    {
        private static string _ConnectionString { get; set; }

        public static void SetBaseConnectionString(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public static string GetConnectionString()
        {
            return _ConnectionString;
        }
    }
}
