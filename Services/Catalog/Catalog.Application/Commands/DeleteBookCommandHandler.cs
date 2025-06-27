using Catalog.Domain.Services;
using MediatR;

namespace Catalog.Application.Commands;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookService _bookService;

    public DeleteBookCommandHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        await _bookService.DeleteAsync(request.Id);
    }
}
