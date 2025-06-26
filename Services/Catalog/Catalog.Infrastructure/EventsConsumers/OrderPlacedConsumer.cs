using MassTransit;
using Shared.Messaging.Events;

namespace Catalog.Infrastructure.EventsConsumers;
public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    public OrderPlacedConsumer()
    {

    }

    public Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        var @event = context.Message;
        var orderId = @event.OrderId;

        return Task.CompletedTask;
    }
}
