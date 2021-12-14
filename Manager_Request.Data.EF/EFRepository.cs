using Manager_Request.Data.EF.Interface;
using Microsoft.EntityFrameworkCore;
using QLHB.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Manager_Request.Data.EF
{
    public class EFRepository<T> : IRepository<T>, IDisposable where T : class
    {
        private readonly AppDbContext _context;

        public EFRepository(AppDbContext context)
        {
            _context = context;
        }

        public T FindById(object id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> FindByIdAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public void AddRange(List<T> entity)
        {
            _context.AddRange(entity);
        }

        public async Task AddRangeAsync(List<T> entity)
        {
            await _context.AddRangeAsync(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Remove(object id)
        {
            Remove(FindById(id));
        }

        public void RemoveMultiple(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void UpdateRange(List<T> entity)
        {
            _context.Set<T>().UpdateRange(entity);
        }

        public void UpdateUnTracker(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
            _context.Set<T>().Update(entity);
        }


        public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.AsNoTracking();
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.AsNoTracking().Where(predicate);
        }



        public T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(includeProperties).FirstOrDefault(predicate);
        }

        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return await FindAll(includeProperties).FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> Queryable()
        {
            return _context.Set<T>().AsQueryable();
        }


        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }


    }
}
