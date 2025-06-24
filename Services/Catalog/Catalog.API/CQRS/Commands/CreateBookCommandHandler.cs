using Catalog.Domain.Services;
using MediatR;

namespace Catalog.API.CQRS.Commands;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
{
    private readonly IBookService _bookService;

    public CreateBookCommandHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        await _bookService.CreateBookAsync(request);
    }
}
