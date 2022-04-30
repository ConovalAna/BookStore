using BookStore.DAL;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ContextDB ContextDB { get; set; }

        public RepositoryBase(ContextDB contextDB)
        {
            this.ContextDB = contextDB;
        }

        public IQueryable<T> FindAll()
        {
            return this.ContextDB.Set<T>().AsNoTracking();
        }

        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.ContextDB.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.ContextDB.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.ContextDB.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.ContextDB.Set<T>().Remove(entity);
        }
    }
}
