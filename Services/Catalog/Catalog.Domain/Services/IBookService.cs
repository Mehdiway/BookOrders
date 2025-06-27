using Shared.DTO;
using Shared.Services;

namespace Catalog.Domain.Services;
public interface IBookService : IGenericService<BookDto>
{
    Task<bool> CheckBookIdsAllExistAsync(List<int> bookIds);
    Task DecreaseBookQuantitiesAsync(Dictionary<int, int> bookQuantities, int orderId);
    Task<bool> IsQuantityAvailableForBookIdAsync(int bookId, int quantity);
}
