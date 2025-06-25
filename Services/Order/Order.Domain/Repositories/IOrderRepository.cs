using Shared.Repositories;

namespace Order.Domain.Repositories;
public interface IOrderRepository : IGenericRepository<Domain.Entities.Order>
{
}
