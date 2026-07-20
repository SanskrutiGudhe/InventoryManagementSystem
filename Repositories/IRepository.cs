using System.Linq.Expressions;

namespace InventoryManagement.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes); // New Method
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
    }
}
