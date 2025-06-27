using Shared.CQRS;
using Shared.DTO;

namespace Catalog.Application.Queries;

public record GetBooksQuery : IQuery<List<BookDto>>
{
}