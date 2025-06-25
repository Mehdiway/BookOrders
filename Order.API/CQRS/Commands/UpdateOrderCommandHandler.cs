using MediatR;
using Order.Domain.Services;

namespace Order.API.CQRS.Commands;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderService _orderService;

    public UpdateOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        await _orderService.UpdateAsync(request);
    }
}
