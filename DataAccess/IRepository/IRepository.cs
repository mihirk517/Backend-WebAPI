using System.Linq.Expressions;

namespace WebAPI.DataAccess.IRepository
{
    public interface IRepository<T> where T :class 
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeprops = null);
        T Get(Expression<Func<T, bool>> filter, string? includeprops=null,bool tracked =false);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
