using MediatR;
using Order.Domain.Services;

namespace Order.API.CQRS.Commands;

public class CheckoutBookCommandHandler : IRequestHandler<CheckoutBookCommand>
{
    private readonly IOrderService _orderService;
    private HttpClient _httpClient;

    public CheckoutBookCommandHandler(IOrderService orderService, IHttpClientFactory httpClientFactory)
    {
        _orderService = orderService;
        _httpClient = httpClientFactory.CreateClient("catalog");
    }

    public async Task Handle(CheckoutBookCommand request, CancellationToken cancellationToken)
    {
        // Validate the bookIds
        var bookIds = request.OrderItems.Select(oi => oi.BookId).ToList();

        var response = await _httpClient.PostAsJsonAsync("/api/Books/BookIdsAllExist", bookIds);
        response.EnsureSuccessStatusCode();

        await _orderService.CheckoutBookAsync(request);
    }
}
