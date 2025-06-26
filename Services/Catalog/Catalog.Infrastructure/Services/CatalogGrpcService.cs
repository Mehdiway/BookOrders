using Ardalis.GuardClauses;
using Catalog.Domain.Repositories;
using Grpc.Core;
using Shared.Grpc;

namespace Catalog.Infrastructure.Services;

public class CatalogGrpcService : CatalogService.CatalogServiceBase
{
    private readonly IBookRepository _bookRepository;

    public CatalogGrpcService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public override async Task<CheckBookQuantityResponse> CheckBookQuantity(CheckBookQuantityRequest request, ServerCallContext context)
    {
        var book = await _bookRepository.GetBookByIdAsync(request.BookId);
        Guard.Against.Null(book);

        return new()
        {
            IsAvailable = book.Quantity >= request.Quantity
        };
    }
}
