using Shared.Exceptions;

namespace Catalog.Domain.Exceptions;
public class InsufficientBookQuantityToDecreaseException : DomainException
{
    public InsufficientBookQuantityToDecreaseException()
        : base("Unable to decrease book quantity. Insufficient quantity.")
    {
    }
}
