using Shared.DTO;
using Shared.CQRS;

namespace Catalog.Application.Commands;

public class CreateBookCommand : BookDto, ICommand;