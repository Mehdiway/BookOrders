using Catalog.Domain.Services;
using MediatR;

namespace Catalog.API.CQRS.Commands;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookService _bookService;

    public DeleteBookCommandHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        await _bookService.DeleteBookAsync(request.Id);
    }
}
