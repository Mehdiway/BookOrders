using Shared.CQRS;

namespace Order.API.CQRS.Commands;

public class DeleteOrderCommand(int Id) : ICommand
{
    public int Id { get; } = Id;
}
