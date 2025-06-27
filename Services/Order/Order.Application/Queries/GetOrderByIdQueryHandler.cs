using Ardalis.GuardClauses;
using MediatR;
using Order.Domain.Services;
using Shared.DTO;

namespace Order.Application.Queries;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderService _orderService;

    public GetOrderByIdQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var orderDto = await _orderService.GetByIdAsync(request.Id);
        Guard.Against.Null(orderDto);
        return orderDto;
    }
}
