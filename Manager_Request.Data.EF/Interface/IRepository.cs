using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Request.Data.EF.Interface
{
    public interface IRepository<T> where T : class
    {
        T FindById(object id);

        Task<T> FindByIdAsync(object id);
        void Add(T entity);

        Task AddAsync(T entity);

        void AddRange(List<T> entity);

        Task AddRangeAsync(List<T> entity);
        void Remove(T entity);

        void Remove(object id);

        void RemoveMultiple(List<T> entities);

        void Update(T entity);

        void UpdateRange(List<T> entity);

        void UpdateUnTracker(T entity);

        T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> Queryable();
    }
}
