using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using WebAPI.Data;
using WebAPI.DataAccess.IRepository;
namespace WebAPI.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _db;
        internal DbSet<T> dbSet;
        public Repository(DatabaseContext db) 
        {
            _db = db;
            this.dbSet = _db.Set<T>();

        }
        void IRepository<T>.Add(T entity)
        {
                      
            dbSet.Add(entity);
        }

        T IRepository<T>.Get(Expression<Func<T, bool>> filter, string? includeprops, bool tracked)
        {
            if (tracked)
            {
                IQueryable<T> query = dbSet;
                if(filter != null) query = query.Where(filter);
                if (!string.IsNullOrEmpty(includeprops))
                {
                    foreach (var props in includeprops.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(props);
                    }
                }
                return query.FirstOrDefault();
            }
            else
            {
                IQueryable<T> query = dbSet.AsNoTracking();
                if (filter != null) query = query.Where(filter);
                if (!string.IsNullOrEmpty(includeprops))
                {
                    foreach (var props in includeprops.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(props);
                    }
                }
                return query.FirstOrDefault();
            }
        }

        IEnumerable<T> IRepository<T>.GetAll(Expression<Func<T, bool>> filter, string? includeprops)
        {
            IQueryable<T> query = dbSet;
            if (filter != null) query = query.Where(filter);

            if(!string.IsNullOrEmpty(includeprops))
            {
                foreach (var props in includeprops.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(props);
                }
            }

            return query.ToList();
        }

        void IRepository<T>.Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        void IRepository<T>.RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
