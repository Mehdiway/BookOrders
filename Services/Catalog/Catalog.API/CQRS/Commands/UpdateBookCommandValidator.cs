using FluentValidation;

namespace Catalog.API.CQRS.Commands;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must have a valid value");

        RuleFor(x => x.Title)
            .MinimumLength(2)
            .MaximumLength(255);

        RuleFor(x => x.Author)
            .MinimumLength(2)
            .MaximumLength(255);

        RuleFor(x => x.PublicationDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Publication date cannot be in the future");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price has to have a positive value");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quantity cannot have a negative value");
    }
}
