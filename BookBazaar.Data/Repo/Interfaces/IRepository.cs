using System.Linq.Expressions;

namespace BookBazaar.Data.Repo.Interfaces;

public interface IRepository<T> where T : class
{
    Task<ICollection<T>> RetrieveAllAsync(Expression<Func<T, bool>>? filter = null, string? includedProperties = null);
    Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includedProperties = null);
    Task<T> CreateAsync(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}