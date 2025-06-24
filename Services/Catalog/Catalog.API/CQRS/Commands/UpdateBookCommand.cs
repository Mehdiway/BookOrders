using Shared.CQRS;
using Shared.DTO;

namespace Catalog.API.CQRS.Commands;

public class UpdateBookCommand : BookDto, ICommand;