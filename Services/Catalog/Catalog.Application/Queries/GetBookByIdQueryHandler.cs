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
        return await _bookService.GetBookByIdAsync(request.Id);
    }
}
