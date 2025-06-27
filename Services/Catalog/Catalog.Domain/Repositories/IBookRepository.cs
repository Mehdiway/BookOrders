using Catalog.Domain.Entities;
using Shared.Repositories;

namespace Catalog.Domain.Repositories;
public interface IBookRepository : IGenericRepository<Book>
{
    Task<bool> CheckBookIdsAllExistAsync(List<int> booksIds);
    Task DecreaseBookQuantitiesAsync(Dictionary<int, int> bookQuantities);
    Task<bool> IsQuantityAvailableForBookIdAsync(int bookId, int quantity);
}
