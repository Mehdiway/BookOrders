using Shared.DTO;
using Shared.CQRS;

namespace Catalog.API.CQRS.Commands;

public class CreateBookCommand : BookDto, ICommand;