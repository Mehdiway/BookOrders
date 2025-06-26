using Catalog.Domain.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Shared.Grpc;

namespace Catalog.Infrastructure.Services;
public class CatalogGrpcService : CatalogService.CatalogServiceBase
{
    private readonly IBookService _bookService;

    public CatalogGrpcService(IBookService bookService)
    {
        _bookService = bookService;
    }

    public override async Task<CheckBookQuantityResponse> CheckBookQuantity(CheckBookQuantityRequest request, ServerCallContext context)
    {
        var isAvailable = await _bookService.IsQuantityAvailableForBookIdAsync(request.BookId, request.Quantity);

        return new CheckBookQuantityResponse()
        {
            IsAvailable = isAvailable
        };
    }

    public override async Task<Empty> DecreaseBookQuantity(DecreaseBookQuantityRequest request, ServerCallContext context)
    {
        var bookQuantities = request.BookQuantities.ToDictionary();
        await _bookService.DecreaseBookQuantitiesAsync(bookQuantities);

        return new Empty();
    }
}
