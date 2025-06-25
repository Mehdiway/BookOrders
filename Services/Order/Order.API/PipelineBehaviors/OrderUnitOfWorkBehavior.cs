using Microsoft.EntityFrameworkCore;
using Order.Infrastructure;
using Shared.PipelineBehaviors;

namespace Order.API.PipelineBehaviors;

public class OrderUnitOfWorkBehavior<TRequest, TResponse> : UnitOfWorkBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly OrderContext _context;

    public OrderUnitOfWorkBehavior(OrderContext context)
    {
        _context = context;
    }

    protected override DbContext GetDbContext() => _context;
}