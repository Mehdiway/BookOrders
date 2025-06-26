using Shared.Exceptions;

namespace Order.Domain.Exceptions;

public class NotAllBookIdsExistException : DomainException
{
    public NotAllBookIdsExistException() : base("Not all book IDs exist in the database.")
    {
    }
}
