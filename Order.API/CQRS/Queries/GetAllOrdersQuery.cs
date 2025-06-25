using Shared.CQRS;
using Shared.DTO;

namespace Order.API.CQRS.Queries;

public class GetAllOrdersQuery : IQuery<List<OrderDto>>
{
}
