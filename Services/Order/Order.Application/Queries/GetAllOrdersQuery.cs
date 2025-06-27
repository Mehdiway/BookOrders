using Shared.CQRS;
using Shared.DTO;

namespace Order.Application.Queries;

public class GetAllOrdersQuery : IQuery<List<OrderDto>>
{
}
