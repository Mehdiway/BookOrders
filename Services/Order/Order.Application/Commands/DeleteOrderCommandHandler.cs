using MediatR;
using Order.Domain.Services;

namespace Order.Application.Commands;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderService _orderService;

    public DeleteOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        await _orderService.DeleteAsync(request.Id);
    }
}
