using Catalog.API.CQRS.Commands;
using Catalog.API.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetBooksQuery(), cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookById([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetBookByIdQuery(id), cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand createBookCommand, CancellationToken cancellationToken)
    {
        await _mediator.Send(createBookCommand, cancellationToken);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand updateBookCommand, CancellationToken cancellationToken)
    {
        await _mediator.Send(updateBookCommand, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBookCommand(id), cancellationToken);
        return Ok();
    }

    [HttpPost("BookIdsAllExist")]
    public async Task<IActionResult> BookIdsAllExist([FromBody] List<int> bookIds, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CheckBookIdsAllExistQuery(bookIds), cancellationToken);
        return Ok(result);
    }
}
