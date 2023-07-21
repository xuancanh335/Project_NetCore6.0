using Common.Commons;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Repository.Queries
{
    public class StoreProcedureExecute : IStoreProcedureExecute
    {
        public string _connectionString { get; set; }

        public StoreProcedureExecute()
        {
            _connectionString = ConfigHelper.Get("ConnectionStrings", "DefaultConnection");
        }

        /// <summary>
        /// </summary>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters"> Array of parameters </param>
        public async Task ExecuteNotReturn(string storeProcedureName, DynamicParameters parameters = null)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                await sql.QueryFirstOrDefaultAsync(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Execute store  return a type
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public T ExecuteReturnScaler<T>(string storeProcedureName, DynamicParameters parameters = null)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                return (T)Convert.ChangeType(sql.ExecuteScalar<T>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
            }
        }

        /// <summary>
        /// Execute store  return a list object
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteReturnList<T>(string storeProcedureName, DynamicParameters parameters = null)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                return await sql.QueryAsync<T>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Execute store  return a list object
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteReturnListBySql<T>(string query)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                return await sql.QueryAsync<T>(query, null, commandType: System.Data.CommandType.Text);
            }
        }
        /// <summary>
        /// Execute store  return a list object
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteReturnListBySql<T>(string query, DynamicParameters parameters = null)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                return await sql.QueryAsync<T>(query, parameters);
            }
        }
    }
}
