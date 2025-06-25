using Order.Domain.Entities;
using Shared.Repositories;

namespace Order.Domain.Repositories;
public interface IOrderItemRepository : IGenericRepository<OrderItem>
{
}
