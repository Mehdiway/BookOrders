using Catalog.Domain.Services;
using MediatR;
using Shared.DTO;

namespace Catalog.Application.Queries;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
{
    private readonly IBookService _bookService;

    public GetBooksQueryHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookService.GetBooksAsync(cancellationToken);
        return books;
    }
}
