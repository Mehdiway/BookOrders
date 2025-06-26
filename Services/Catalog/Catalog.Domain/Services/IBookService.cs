using Shared.DTO;

namespace Catalog.Domain.Services;
public interface IBookService
{
    Task<bool> CheckBookIdsAllExistAsync(List<int> bookIds);
    Task CreateBookAsync(BookDto bookDto);
    Task DecreaseBookQuantitiesAsync(Dictionary<int, int> bookQuantities);
    Task DeleteBookAsync(int id);
    Task<BookDto> GetBookByIdAsync(int id);
    public Task<List<BookDto>> GetBooksAsync(CancellationToken cancellationToken);
    Task<bool> IsQuantityAvailableForBookIdAsync(int bookId, int quantity);
    Task UpdateBookAsync(BookDto bookDto);
}
