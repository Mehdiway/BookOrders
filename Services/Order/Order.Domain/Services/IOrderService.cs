using Shared.DTO;
using Shared.Services;

namespace Order.Domain.Services;
public interface IOrderService : IGenericService<OrderDto>
{
    Task CheckoutBookAsync(OrderDto order);
    Task ConfirmOrderAsync(int orderId);
    Task CancelOrderAsync(int orderId);
}
