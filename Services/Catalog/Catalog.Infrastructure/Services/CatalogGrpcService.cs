using Catalog.Domain.Services;
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
}
