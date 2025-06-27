using Catalog.Domain.Services;
using MediatR;

namespace Catalog.Application.Commands;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookService _bookService;

    public UpdateBookCommandHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        await _bookService.UpdateAsync(request);
    }
}
