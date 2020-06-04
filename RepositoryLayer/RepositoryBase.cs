using Models;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected GrubNowDbContext _context;
        public RepositoryBase(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<GrubNowDbContext>();
        }
        public virtual IEnumerable<T> Get()
        {
            return _context.Set<T>().AsQueryable();
        }
        public virtual T GetById(int id)
        {
            return  _context.Set<T>().Find(id);
        }
        public virtual IEnumerable<T> GetWithCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public virtual async Task<int> Post(T entity, bool saveChanges = true)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }
        public virtual async Task<int> Put(T entity, bool saveChanges = true)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
        }
        public virtual async Task<int> Delete(int id, bool saveChanges = true)
        {
            var entity = GetById(id);
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public virtual async Task<int> DeleteRange(IEnumerable<int> entityIDs, bool saveChanges = true)
        {
            foreach (var id in entityIDs)
            {
                var entity = GetById(id);
                _context.Set<T>().Remove(entity);
            }
            return await _context.SaveChangesAsync();
        }

        public bool Any(int id)
        {
            var prod = GetById(id);
            return prod != null ? true : false;
        }
    }
}
