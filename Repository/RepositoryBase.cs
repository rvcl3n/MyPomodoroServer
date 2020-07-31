using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public Task Create(T entity)
        {
            return Task.Run(() => {
                RepositoryContext.Set<T>().Add(entity);
            });
        }

        public Task Update(T entity)
        {
            return Task.Run(() => {
                RepositoryContext.Set<T>().Update(entity);
            });
        }

        public Task Delete(T entity)
        {
            return Task.Run(() => {
                RepositoryContext.Set<T>().Remove(entity);
            });
        }
    }
}
