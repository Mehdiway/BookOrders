using Ardalis.GuardClauses;
using AutoMapper;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Domain.Services;
using Shared.DTO;

namespace Catalog.Infrastructure.Services;
public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task CreateBookAsync(BookDto bookDto)
    {
        Book book = _mapper.Map<Book>(bookDto);
        await _bookRepository.CreateBookAsync(book);
    }

    public async Task<BookDto> GetBookByIdAsync(int id)
    {
        Book? book = await _bookRepository.GetBookByIdAsync(id);
        Guard.Against.Null(book);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<List<BookDto>> GetBooksAsync(CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetBooksAsync(cancellationToken);
        return _mapper.Map<List<BookDto>>(books);
    }

    public async Task UpdateBookAsync(BookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        await _bookRepository.UpdateBookAsync(book);
    }

    public async Task DeleteBookAsync(int id)
    {
        await _bookRepository.DeleteBookAsync(id);
    }
}
