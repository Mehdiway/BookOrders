using Shared.Exceptions;

namespace Order.Domain.Exceptions;

public class InsufficientBookQuantityException : DomainException
{
    public InsufficientBookQuantityException() : base("Insufficient book quantity.")
    {
    }
}
