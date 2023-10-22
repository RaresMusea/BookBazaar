using System.Linq.Expressions;

namespace BookBazaar.Data.Repo.Interfaces;

public interface IRepository<T> where T : class
{
    Task<ICollection<T>> RetrieveAllAsync();
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task<T> CreateAsync(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}