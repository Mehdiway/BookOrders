using AutoMapper;
using Shared.Entities;
using Shared.Repositories;

namespace Shared.Services;
public class GenericService<T, TDto> : IGenericService<TDto>
    where T : Entity
    where TDto : class
{
    private readonly IGenericRepository<T> _repository;
    private readonly IMapper _mapper;

    public GenericService(IGenericRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<List<TDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _repository.GetAllAsync(cancellationToken: cancellationToken);
        return _mapper.Map<List<TDto>>(list);
    }

    public virtual async Task<TDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken: cancellationToken);
        return _mapper.Map<TDto?>(entity);
    }

    public virtual async Task<TDto> AddAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<T>(dto);
        var newEntity = await _repository.AddAsync(entity, cancellationToken);
        return _mapper.Map<TDto>(newEntity);
    }

    public virtual async Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        var newEntity = await _repository.UpdateAsync(_mapper.Map<T>(dto), cancellationToken);
        return _mapper.Map<TDto>(newEntity);
    }

    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
}
