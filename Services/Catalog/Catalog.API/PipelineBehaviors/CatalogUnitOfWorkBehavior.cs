using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.PipelineBehaviors;

namespace Catalog.API.PipelineBehaviors;

public class CatalogUnitOfWorkBehavior<TRequest, TResponse> : UnitOfWorkBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly CatalogContext _context;

    public CatalogUnitOfWorkBehavior(CatalogContext context)
    {
        _context = context;
    }

    protected override DbContext GetDbContext() => _context;
}