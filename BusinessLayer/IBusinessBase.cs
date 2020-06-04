using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IBusinessBase<T> where T : class
    {
        IEnumerable<T> Get();
        T GetById(int id);
        IEnumerable<T> GetWithCondition(Expression<Func<T, bool>> expression);
        Task<int> Post(T entity, bool saveChanges = true);
        Task<int> Put(int id, T entity, bool saveChanges = true);
        Task<int> Delete(int id, bool saveChanges = true);
        Task<int> DeleteRange(IEnumerable<int> entityIDs, bool saveChanges = true);
    }
}
