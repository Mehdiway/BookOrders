using Grpc.Net.Client;
using MediatR;
using Order.Domain.Exceptions;
using Order.Domain.Services;
using Shared.Grpc;

namespace Order.API.CQRS.Commands;

public class CheckoutBookCommandHandler : IRequestHandler<CheckoutBookCommand>
{
    private readonly IOrderService _orderService;
    private HttpClient _httpClient;
    private CatalogService.CatalogServiceClient _client;

    public CheckoutBookCommandHandler(IOrderService orderService, IHttpClientFactory httpClientFactory, GrpcChannel channel)
    {
        _orderService = orderService;
        _httpClient = httpClientFactory.CreateClient("catalog");

        _client = new CatalogService.CatalogServiceClient(channel);
    }

    public async Task Handle(CheckoutBookCommand request, CancellationToken cancellationToken)
    {
        // Validate the bookIds
        var bookIds = request.OrderItems.Select(oi => oi.BookId).ToList();

        var response = await _httpClient.PostAsJsonAsync("/api/Books/BookIdsAllExist", bookIds);
        response.EnsureSuccessStatusCode();

        // Validate quantities for each book Id (using gRPC)
        foreach (var orderItem in request.OrderItems)
        {
            await ValidateBookQuantityUsingGrpcAsync(orderItem.BookId, orderItem.Quantity);
        }

        await _orderService.CheckoutBookAsync(request);

        await DecreaseBookQuantitiesAsync(request);
    }

    private async Task ValidateBookQuantityUsingGrpcAsync(int bookId, int quantity)
    {
        var request = new CheckBookQuantityRequest { BookId = bookId, Quantity = quantity };
        var response = await _client.CheckBookQuantityAsync(request);

        if (!response.IsAvailable)
        {
            throw new InsufficientBookQuantityException();
        }
    }

    private async Task DecreaseBookQuantitiesAsync(CheckoutBookCommand command)
    {
        var request = new DecreaseBookQuantityRequest();

        foreach (var orderItem in command.OrderItems)
        {
            request.BookQuantities.Add(orderItem.BookId, orderItem.Quantity);
        }
        var response = await _client.DecreaseBookQuantityAsync(request);
    }
}
