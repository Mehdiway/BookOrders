using Ardalis.GuardClauses;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Repositories;

namespace Catalog.Infrastructure.Repositories;
public class BookRepository : GenericRepository<Book>, IBookRepository, IGenericRepository<Book>
{
    private readonly CatalogContext _context;

    public BookRepository(CatalogContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> CheckBookIdsAllExistAsync(List<int> bookIds)
    {
        var count = await _context.Books.Where(b => bookIds.Contains(b.Id)).CountAsync();
        return count == bookIds.Count;
    }

    public async Task<bool> IsQuantityAvailableForBookIdAsync(int bookId, int quantity)
    {
        var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == bookId);
        Guard.Against.Null(book);
        return book.Quantity >= quantity;
    }

    public async Task DecreaseBookQuantitiesAsync(Dictionary<int, int> bookQuantities)
    {
        foreach (var bookQuantity in bookQuantities)
        {
            var book = await _context.Books.FindAsync(bookQuantity.Key);
            Guard.Against.Null(book);
            if (book.Quantity < bookQuantity.Value)
            {
                throw new InsufficientBookQuantityToDecreaseException();
            }
            book.Quantity -= bookQuantity.Value;
        }
        await _context.SaveChangesAsync();
    }
}
