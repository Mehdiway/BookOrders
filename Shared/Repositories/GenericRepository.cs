using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Shared.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : Entity
{
    private readonly DbContext _context;
    private readonly DbSet<T> _table;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public virtual async Task<List<T>> GetAllAsync(string? includeProperty = "", CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(includeProperty))
        {
            return await _table.ToListAsync();
        }

        return await _table.Include(includeProperty).ToListAsync(cancellationToken);
    }

    public virtual async Task<T?> GetByIdAsync(int id, string? includeProperty = "", CancellationToken cancellationToken = default)
    {
        T? entity = null;

        if (string.IsNullOrEmpty(includeProperty))
        {
            entity = await _table.FindAsync(id);
        }
        else
        {
            entity = await _table.Include(includeProperty).Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        Guard.Against.Null(entity);
        return entity;
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _table.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _table.FindAsync(id);
        Guard.Against.Null(entity);
        _table.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _table.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}
