using Shared.CQRS;
using Shared.DTO;

namespace Catalog.API.CQRS.Queries;

public record GetBooksQuery : IQuery<List<BookDto>>
{
}