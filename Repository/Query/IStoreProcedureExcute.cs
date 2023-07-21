using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Queries
{
    public interface IStoreProcedureExecute
    {
        Task<IEnumerable<T>> ExecuteReturnListBySql<T>(string query);
        Task ExecuteNotReturn(string storeProcedureName, DynamicParameters parameters = null);
        T ExecuteReturnScaler<T>(string storeProcedureName, DynamicParameters parameters = null);
        Task<IEnumerable<T>> ExecuteReturnListBySql<T>(string query, DynamicParameters parameters = null);
        Task<IEnumerable<T>> ExecuteReturnList<T>(string storeProcedureName, DynamicParameters parameters = null);
    }
}
