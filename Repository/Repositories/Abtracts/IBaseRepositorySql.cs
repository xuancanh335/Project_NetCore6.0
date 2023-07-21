using Common.Params.Base;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IBaseRepositorySql<T> : IDisposable where T : class
    {
        Task<T> Create(T obj);
        Task<T> GetById(object id);
        Task<bool> Delete(object id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Update(T obj, object id);
        Task<bool> CheckExists(object id);
        Task<ListResult<T>> GetAll(PagingParam param);
        Task<bool> UpdateIsActive(bool isActive, object id);
        Task<T> GetSingle(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetList(Expression<Func<T, bool>> predicate);
        Task<bool> CheckExistsAnyAsync(Expression<Func<T, bool>> predicate);
    }
}