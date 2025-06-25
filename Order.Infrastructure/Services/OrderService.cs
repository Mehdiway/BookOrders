using AutoMapper;
using Order.Domain.Repositories;
using Order.Domain.Services;
using Shared.DTO;
using Shared.Services;

namespace Order.Infrastructure.Services;
public class OrderService : GenericService<Domain.Entities.Order, OrderDto>, IOrderService, IGenericService<OrderDto>
{
    public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemsRepository, IMapper mapper) : base(orderRepository, mapper)
    {
    }

    public async Task CheckoutBookAsync(OrderDto orderDto)
    {
        await AddAsync(orderDto);
    }
}
