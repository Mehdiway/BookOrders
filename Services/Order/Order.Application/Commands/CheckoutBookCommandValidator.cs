using FluentValidation;
using Shared.Enums;

namespace Order.Application.Commands;

public class CheckoutBookCommandValidator : AbstractValidator<CheckoutBookCommand>
{
    public CheckoutBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .Equal(0)
            .WithMessage("Id cannot have a value other than 0");

        RuleFor(x => x.OrderItems)
            .NotEmpty()
            .WithMessage("An Order has to have at least one Order Item");

        RuleFor(x => x.OrderStatus)
            .Equal(OrderStatus.Waiting)
            .WithMessage("New checkout must have the status Waiting (orderStatus = 1)");
    }
}
