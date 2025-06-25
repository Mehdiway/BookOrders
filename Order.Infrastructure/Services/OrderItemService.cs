using AutoMapper;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Order.Domain.Services;
using Shared.DTO;
using Shared.Services;

namespace Order.Infrastructure.Services;
public class OrderItemService : GenericService<OrderItem, OrderItemDto>, IOrderItemService, IGenericService<OrderItemDto>
{
    public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper) : base(orderItemRepository, mapper)
    {
    }
}
