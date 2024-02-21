using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(string id);
        List<T> GetAll();
        List<T> Find(Expression<Func<T,bool>> predicate);
        void Add(T entity);
        void AddRange(List<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(List<T> entities);
    }
}
