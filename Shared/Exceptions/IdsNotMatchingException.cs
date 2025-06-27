namespace Shared.Exceptions;

public class IdsNotMatchingException : DomainException
{
    public IdsNotMatchingException() : base("Ids aren't matching.") { }
}