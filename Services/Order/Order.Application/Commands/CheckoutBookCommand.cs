using Shared.CQRS;
using Shared.DTO;

namespace Order.Application.Commands;

public class CheckoutBookCommand : OrderDto, ICommand;