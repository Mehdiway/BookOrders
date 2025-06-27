namespace Shared.Services;
public interface IGenericService<TDto> where TDto : class
{
    Task<List<TDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TDto> AddAsync(TDto dto, CancellationToken cancellationToken = default);
    Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
