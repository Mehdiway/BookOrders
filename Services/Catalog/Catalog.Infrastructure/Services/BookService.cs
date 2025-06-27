using AutoMapper;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Domain.Services;
using Microsoft.Extensions.Logging;
using Shared.DTO;
using Shared.Messaging;
using Shared.Messaging.Events;
using Shared.Services;

namespace Catalog.Infrastructure.Services;
public class BookService : GenericService<Book, BookDto>, IBookService, IGenericService<BookDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IEventPublisher _publisher;
    private readonly ILogger<BookService> _logger;

    public BookService(IBookRepository bookRepository, IMapper mapper, IEventPublisher publisher, ILogger<BookService> logger) : base(bookRepository, mapper)
    {
        _bookRepository = bookRepository;
        _publisher = publisher;
        _logger = logger;
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
            _logger.LogError(ex, "Unable to decrease book quantities");
            BookQuantitiesCannotBeDecreasedEvent @event = new()
            {
                OrderId = orderId
            };
            await _publisher.PublishAsync(@event);
        }
    }
}
