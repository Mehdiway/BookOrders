using Shared.CQRS;
using Shared.DTO;

namespace Catalog.Application.Commands;

public class UpdateBookCommand : BookDto, ICommand;