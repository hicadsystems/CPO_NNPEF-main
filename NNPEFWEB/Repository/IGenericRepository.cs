using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public interface IGenericRepository<T>
    {
        Task<T> Find(int id);
        IEnumerable<T> GetByExpression(Expression<Func<T, bool>> predicate);
        IEnumerable<T> All();
        Task<int> Count();
        Task<int> Count(Expression<Func<T, bool>> predicate);
        bool Create(T entity);
        bool CreateRange(IEnumerable<T> entities);
        bool Remove(T entity);
        bool Update(T entity);
        bool UpdateRange(IEnumerable<T> entities);
        bool RemoveRange(IEnumerable<T> entities);
    }
}
