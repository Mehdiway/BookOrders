using Ardalis.GuardClauses;
using AutoMapper;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Domain.Services;
using Shared.DTO;
using Shared.Messaging;
using Shared.Messaging.Events;

namespace Catalog.Infrastructure.Services;
public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _publisher;

    public BookService(IBookRepository bookRepository, IMapper mapper, IEventPublisher publisher)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _publisher = publisher;
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

    public async Task<bool> CheckBookIdsAllExistAsync(List<int> bookIds)
    {
        var result = await _bookRepository.CheckBookIdsAllExistAsync(bookIds);
        return result;
    }

    public async Task<bool> IsQuantityAvailableForBookIdAsync(int bookId, int quantity)
    {
        return await _bookRepository.IsQuantityAvailableForBookIdAsync(bookId, quantity);
    }

    public async Task DecreaseBookQuantitiesAsync(Dictionary<int, int> bookQuantities, int orderId)
    {
        try
        {
            await _bookRepository.DecreaseBookQuantitiesAsync(bookQuantities);

            BookQuantitiesDecreasedEvent @event = new()
            {
                OrderId = orderId
            };
            await _publisher.PublishAsync(@event);
        }
        catch (Exception ex)
        {
            BookQuantitiesCannotBeDecreasedEvent @event = new()
            {
                OrderId = orderId
            };
            await _publisher.PublishAsync(@event);
        }
    }
}
