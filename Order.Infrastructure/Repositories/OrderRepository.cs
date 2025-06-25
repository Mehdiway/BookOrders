using Order.Domain.Repositories;
using Shared.Repositories;

namespace Order.Infrastructure.Repositories;
public class OrderRepository : GenericRepository<Domain.Entities.Order>, IOrderRepository, IGenericRepository<Domain.Entities.Order>
{

    public OrderRepository(OrderContext context) : base(context)
    {
    }

    public override async Task<List<Domain.Entities.Order>> GetAllAsync(string? includeProperty = "")
    {
        return await base.GetAllAsync(nameof(Domain.Entities.Order.OrderItems));
    }

    public override async Task<Domain.Entities.Order?> GetByIdAsync(int id, string? includeProperty = "")
    {
        return await base.GetByIdAsync(id, nameof(Domain.Entities.Order.OrderItems));
    }
}
