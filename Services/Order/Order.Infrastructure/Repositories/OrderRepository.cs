using Ardalis.GuardClauses;
using Order.Domain.Repositories;
using Shared.Repositories;

namespace Order.Infrastructure.Repositories;
public class OrderRepository : GenericRepository<Domain.Entities.Order>, IOrderRepository, IGenericRepository<Domain.Entities.Order>
{
    private readonly OrderContext _context;

    public OrderRepository(OrderContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<List<Domain.Entities.Order>> GetAllAsync(string? includeProperty = "", CancellationToken cancellationToken = default)
    {
        return await base.GetAllAsync(nameof(Domain.Entities.Order.OrderItems));
    }

    public override async Task<Domain.Entities.Order?> GetByIdAsync(int id, string? includeProperty = "", CancellationToken cancellationToken = default)
    {
        return await base.GetByIdAsync(id, nameof(Domain.Entities.Order.OrderItems));
    }

    public async Task ConfirmOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        Guard.Against.Null(order);

        order.OrderStatus = Shared.Enums.OrderStatus.Confirmed;

        await _context.SaveChangesAsync();
    }

    public async Task CancelOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        Guard.Against.Null(order);

        order.OrderStatus = Shared.Enums.OrderStatus.Cancelled;

        await _context.SaveChangesAsync();
    }
}
