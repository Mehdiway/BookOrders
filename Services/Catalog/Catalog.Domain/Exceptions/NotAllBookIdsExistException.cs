using Shared.Exceptions;

namespace Catalog.Domain.Exceptions;

public class NotAllBookIdsExistException : DomainException
{
    public NotAllBookIdsExistException() : base("Not all book IDs exist in the database.")
    {
    }
}
