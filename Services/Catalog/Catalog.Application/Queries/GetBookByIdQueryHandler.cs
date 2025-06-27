using Ardalis.GuardClauses;
using Catalog.Domain.Services;
using MediatR;
using Shared.DTO;

namespace Catalog.Application.Queries;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
{
    private readonly IBookService _bookService;

    public GetBookByIdQueryHandler(IBookService bookService)
    {
        _bookService = bookService;
    }
    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookService.GetByIdAsync(request.Id);
        Guard.Against.Null(book);
        return book;
    }
}
