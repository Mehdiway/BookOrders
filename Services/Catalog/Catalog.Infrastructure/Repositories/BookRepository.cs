using Ardalis.GuardClauses;
using AutoMapper;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;
public class BookRepository : IBookRepository
{
    private readonly CatalogContext _context;
    private readonly IMapper _mapper;

    public BookRepository(CatalogContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task CreateBookAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken)
    {
        return await _context.Books.ToListAsync(cancellationToken);
    }

    public async Task UpdateBookAsync(Book book)
    {
        var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
        Guard.Against.Null(existingBook);

        _mapper.Map(book, existingBook);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(int id)
    {
        var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        Guard.Against.Null(existingBook);

        _context.Books.Remove(existingBook);

        await _context.SaveChangesAsync();
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
}
