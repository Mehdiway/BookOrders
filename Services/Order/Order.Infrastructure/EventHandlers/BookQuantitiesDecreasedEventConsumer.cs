using MassTransit;
using Order.Domain.Services;
using Shared.Messaging.Events;

namespace Order.Infrastructure.EventHandlers;
public class BookQuantitiesDecreasedEventConsumer : IConsumer<BookQuantitiesDecreasedEvent>
{
    private readonly IOrderService _orderService;

    public BookQuantitiesDecreasedEventConsumer(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task Consume(ConsumeContext<BookQuantitiesDecreasedEvent> context)
    {
        var orderId = context.Message.OrderId;
        await _orderService.ConfirmOrderAsync(orderId);
    }
}
