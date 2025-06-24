using Shared.DTO;

namespace Catalog.Domain.Services;
public interface IBookService
{
    Task CreateBookAsync(BookDto bookDto);
    Task DeleteBookAsync(int id);
    Task<BookDto> GetBookByIdAsync(int id);
    public Task<List<BookDto>> GetBooksAsync(CancellationToken cancellationToken);
    Task UpdateBookAsync(BookDto bookDto);
}
