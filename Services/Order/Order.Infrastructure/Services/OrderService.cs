using AutoMapper;
using Order.Domain.Repositories;
using Order.Domain.Services;
using Shared.DTO;
using Shared.Messaging;
using Shared.Messaging.Events;
using Shared.Services;

namespace Order.Infrastructure.Services;
public class OrderService : GenericService<Domain.Entities.Order, OrderDto>, IOrderService, IGenericService<OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEventPublisher _publisher;

    public OrderService(IOrderRepository orderRepository,
        IOrderItemRepository orderItemsRepository,
        IEventPublisher publisher,
        IMapper mapper) : base(orderRepository, mapper)
    {
        _orderRepository = orderRepository;
        _publisher = publisher;
    }

    public async Task CheckoutBookAsync(OrderDto orderDto)
    {
        orderDto = await AddAsync(orderDto);

        var @event = new OrderPlacedEvent
        {
            OrderId = orderDto.Id,
            ShippingAddress = orderDto.ShippingAddress,
            OrderStatus = orderDto.OrderStatus,
            OrderItems = orderDto.OrderItems,
        };
        await _publisher.PublishAsync(@event);
    }

    public async Task ConfirmOrderAsync(int orderId)
    {
        await _orderRepository.ConfirmOrderAsync(orderId);
    }

    public async Task CancelOrderAsync(int orderId)
    {
        await _orderRepository.CancelOrderAsync(orderId);
    }
}
