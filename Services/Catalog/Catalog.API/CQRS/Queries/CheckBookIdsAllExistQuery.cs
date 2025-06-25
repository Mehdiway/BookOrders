using Shared.CQRS;

namespace Catalog.API.CQRS.Queries;

public class CheckBookIdsAllExistQuery(List<int> BookIds) : IQuery<bool>
{
    public List<int> BookIds { get; } = BookIds;
}
