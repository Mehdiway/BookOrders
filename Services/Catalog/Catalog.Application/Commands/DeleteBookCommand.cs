using Shared.CQRS;

namespace Catalog.Application.Commands;

public class DeleteBookCommand : ICommand
{
    public DeleteBookCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
