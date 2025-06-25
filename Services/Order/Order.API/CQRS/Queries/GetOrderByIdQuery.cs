using Shared.CQRS;
using Shared.DTO;

namespace Order.API.CQRS.Queries;

public class GetOrderByIdQuery(int Id) : IQuery<OrderDto>
{
    public int Id { get; } = Id;
}
