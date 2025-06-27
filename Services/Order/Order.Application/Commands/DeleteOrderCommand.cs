using Shared.CQRS;

namespace Order.Application.Commands;

public class DeleteOrderCommand(int Id) : ICommand
{
    public int Id { get; } = Id;
}
