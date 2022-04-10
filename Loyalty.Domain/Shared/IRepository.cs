using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Loyalty.Domain
{
    public interface IRepository<T> where T : class
    {
        Task Add(T enttity);
        Task AddWithComplete(T enttity);
        Task Update(T entity);
        Task Delete(object id);
        Task Delete(Expression<Func<T, bool>> where);
        Task Delete(T entity);
        Task DeleteWithComplete(T entity);
        Task<T> GetById(object id);
        Task<T> GetAsync(Expression<Func<T, bool>> where);
        T Get(Expression<Func<T, bool>> where);
        Task<IQueryable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);
        Task<IQueryable<T>> GetManyAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetMany(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

    }
}
