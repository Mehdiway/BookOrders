using Catalog.Domain.Services;
using MediatR;

namespace Catalog.API.CQRS.Queries;

public class CheckBookIdsAllExistQueryHandler : IRequestHandler<CheckBookIdsAllExistQuery, bool>
{
    private readonly IBookService _bookService;

    public CheckBookIdsAllExistQueryHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<bool> Handle(CheckBookIdsAllExistQuery request, CancellationToken cancellationToken)
    {
        var allExist = await _bookService.CheckBookIdsAllExistAsync(request.BookIds);
        return allExist;
    }
}
