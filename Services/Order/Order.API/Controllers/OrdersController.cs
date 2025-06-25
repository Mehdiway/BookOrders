using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.API.CQRS.Commands;
using Order.API.CQRS.Queries;

namespace Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllOrdersQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPost("CheckoutBook")]
    public async Task<IActionResult> CheckoutBook([FromBody] CheckoutBookCommand checkoutBookCommand, CancellationToken cancellationToken)
    {
        await _mediator.Send(checkoutBookCommand, cancellationToken);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] UpdateOrderCommand updateOrderCommand, CancellationToken cancellationToken)
    {
        updateOrderCommand.Id = id;
        await _mediator.Send(updateOrderCommand, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteOrderCommand(id), cancellationToken);
        return Ok();
    }
}
