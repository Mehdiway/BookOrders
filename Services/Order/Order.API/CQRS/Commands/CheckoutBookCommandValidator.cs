using FluentValidation;

namespace Order.API.CQRS.Commands;

public class CheckoutBookCommandValidator : AbstractValidator<CheckoutBookCommand>
{
    public CheckoutBookCommandValidator()
    {
        RuleFor(x => x.OrderItems)
            .NotEmpty()
            .WithMessage("An Order has to have at least one Order Item");
    }
}
