using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> Get();
        T GetById(int id);
        IEnumerable<T> GetWithCondition(Expression<Func<T, bool>> expression);
        Task<int> Post(T entity, bool saveChanges = true);
        Task<int> Put(T entity, bool saveChanges = true);
        Task<int> Delete(int id, bool saveChanges = true);
        Task<int> DeleteRange(IEnumerable<int> entityIDs, bool saveChanges = true);
        bool Any(int id);
    }
}
