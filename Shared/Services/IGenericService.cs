namespace Shared.Services;
public interface IGenericService<TDto> where TDto : class
{
    Task<List<TDto>> GetAllAsync();
    Task<TDto?> GetByIdAsync(int id);
    Task<TDto> AddAsync(TDto dto);
    Task<TDto> UpdateAsync(TDto dto);
    Task DeleteAsync(int id);
}
