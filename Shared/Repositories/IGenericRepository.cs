using Shared.Entities;

namespace Shared.Repositories;
public interface IGenericRepository<T> where T : Entity
{
    Task<List<T>> GetAllAsync(string? includeProperty = "", CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id, string? includeProperty = "", CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
