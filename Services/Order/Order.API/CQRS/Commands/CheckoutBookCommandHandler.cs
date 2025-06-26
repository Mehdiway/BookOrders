using MediatR;
using Order.Domain.Services;
using Shared.Messaging.Configuration;
using Shared.Messaging.Events;

namespace Order.API.CQRS.Commands;

public class CheckoutBookCommandHandler : IRequestHandler<CheckoutBookCommand>
{
    private readonly IOrderService _orderService;
    private readonly IEventPublisher _eventPublisher;
    private HttpClient _httpClient;

    public CheckoutBookCommandHandler(IOrderService orderService, IHttpClientFactory httpClientFactory, IEventPublisher eventPublisher)
    {
        _orderService = orderService;
        _eventPublisher = eventPublisher;
        _httpClient = httpClientFactory.CreateClient("catalog");
    }

    public async Task Handle(CheckoutBookCommand request, CancellationToken cancellationToken)
    {
        // Validate the bookIds
        var bookIds = request.OrderItems.Select(oi => oi.BookId).ToList();

        var response = await _httpClient.PostAsJsonAsync("/api/Books/BookIdsAllExist", bookIds, cancellationToken);
        response.EnsureSuccessStatusCode();

        await _orderService.CheckoutBookAsync(request);

        var @orderPlacedEvent = new OrderPlacedEvent
        {
            OrderId = request.Id,
            ShippingAddress = request.ShippingAddress,
            OrderItems = request.OrderItems
        };
        await _eventPublisher.PublishAsync(orderPlacedEvent, cancellationToken);
    }
}
