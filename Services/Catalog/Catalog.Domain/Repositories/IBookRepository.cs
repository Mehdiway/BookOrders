using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories;
public interface IBookRepository
{
    Task CreateBookAsync(Book book);
    Task DeleteBookAsync(int id);
    Task<Book?> GetBookByIdAsync(int id);
    Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken);
    Task UpdateBookAsync(Book book);
}
