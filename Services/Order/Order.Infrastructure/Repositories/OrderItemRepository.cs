using Order.Domain.Entities;
using Order.Domain.Repositories;
using Shared.Repositories;

namespace Order.Infrastructure.Repositories;
public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository, IGenericRepository<OrderItem>
{
    public OrderItemRepository(OrderContext context) : base(context)
    {
    }
}
