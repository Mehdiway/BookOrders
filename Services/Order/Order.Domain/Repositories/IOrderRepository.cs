using Shared.Repositories;

namespace Order.Domain.Repositories;
public interface IOrderRepository : IGenericRepository<Domain.Entities.Order>
{
    Task ConfirmOrderAsync(int orderId);
    Task CancelOrderAsync(int orderId);
}
