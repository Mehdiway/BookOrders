using Shared.CQRS;
using Shared.DTO;

namespace Catalog.Application.Queries;

public record GetBookByIdQuery : IQuery<BookDto>
{
    public GetBookByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
