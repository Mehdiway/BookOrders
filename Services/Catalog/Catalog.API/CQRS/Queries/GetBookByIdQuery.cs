using Shared.CQRS;
using Shared.DTO;

namespace Catalog.API.CQRS.Queries;

public record GetBookByIdQuery : IQuery<BookDto>
{
    public GetBookByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
