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
        await ValidateCheckout(request);

        await _orderService.CheckoutBookAsync(request);

        await DecreaseBookQuantitiesWithGrpcAsync(request);
    }

    private async Task ValidateCheckout(CheckoutBookCommand request)
    {
        // Validate the bookIds
        var bookIds = request.OrderItems.Select(oi => oi.BookId).ToList();

        var response = await _httpClient.PostAsJsonAsync("/api/Books/BookIdsAllExist", bookIds);
        var allExist = await response.Content.ReadFromJsonAsync<bool>();
        if (!allExist)
        {
            throw new NotAllBookIdsExistException();
        }

        // Validate quantities for each book Id (using gRPC)
        foreach (var orderItem in request.OrderItems)
        {
            await ValidateBookQuantityUsingGrpcAsync(orderItem.BookId, orderItem.Quantity);
        }
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

    private async Task DecreaseBookQuantitiesWithGrpcAsync(CheckoutBookCommand command)
    {
        var request = new DecreaseBookQuantityRequest();

        foreach (var orderItem in command.OrderItems)
        {
            request.BookQuantities.Add(orderItem.BookId, orderItem.Quantity);
        }
        var response = await _client.DecreaseBookQuantityAsync(request);
    }
}
