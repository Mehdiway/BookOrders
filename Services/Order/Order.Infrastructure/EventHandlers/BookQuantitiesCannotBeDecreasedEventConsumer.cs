using MassTransit;
using Order.Domain.Services;
using Shared.Messaging.Events;

namespace Order.Infrastructure.EventHandlers;
public class BookQuantitiesCannotBeDecreasedEventConsumer : IConsumer<BookQuantitiesCannotBeDecreasedEvent>
{
    private readonly IOrderService _orderService;

    public BookQuantitiesCannotBeDecreasedEventConsumer(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task Consume(ConsumeContext<BookQuantitiesCannotBeDecreasedEvent> context)
    {
        var orderId = context.Message.OrderId;
        await _orderService.CancelOrderAsync(orderId);
    }
}
