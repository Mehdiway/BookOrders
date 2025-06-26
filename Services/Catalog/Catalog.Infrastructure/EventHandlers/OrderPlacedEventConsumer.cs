using Catalog.Domain.Services;
using MassTransit;
using Shared.Messaging.Events;

namespace Catalog.Infrastructure.EventHandlers;
public class OrderPlacedEventConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly IBookService _bookService;

    public OrderPlacedEventConsumer(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        OrderPlacedEvent orderPlacedData = context.Message;
        var orderItems = orderPlacedData.OrderItems;


        var bookQuantities = orderItems.ToDictionary(oi => oi.BookId, oi => oi.Quantity);
        await _bookService.DecreaseBookQuantitiesAsync(bookQuantities, orderPlacedData.OrderId);
    }
}
