using Shared.Entities;

namespace Shared.Repositories;
public interface IGenericRepository<T> where T : Entity
{
    Task<List<T>> GetAllAsync(string? includeProperty = "");
    Task<T?> GetByIdAsync(int id, string? includeProperty = "");
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
