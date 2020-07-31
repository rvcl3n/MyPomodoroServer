using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
