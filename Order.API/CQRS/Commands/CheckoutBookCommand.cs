using Shared.CQRS;
using Shared.DTO;

namespace Order.API.CQRS.Commands;

public class CheckoutBookCommand : OrderDto, ICommand;