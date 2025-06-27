using Shared.CQRS;

namespace Catalog.Application.Queries;

public class CheckBookIdsAllExistQuery(List<int> BookIds) : IQuery<bool>
{
    public List<int> BookIds { get; } = BookIds;
}
