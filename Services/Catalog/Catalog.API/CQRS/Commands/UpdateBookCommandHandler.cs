using Catalog.Domain.Services;
using MediatR;

namespace Catalog.API.CQRS.Commands;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookService _bookService;

    public UpdateBookCommandHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        await _bookService.UpdateBookAsync(request);
    }
}
