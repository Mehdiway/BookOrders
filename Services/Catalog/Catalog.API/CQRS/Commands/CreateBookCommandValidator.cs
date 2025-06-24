using FluentValidation;

namespace Catalog.API.CQRS.Commands;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .Equal(0)
            .WithMessage("Id cannot have a value other than 0");

        RuleFor(x => x.Title)
            .MinimumLength(2)
            .MaximumLength(255);

        RuleFor(x => x.Author)
            .MinimumLength(2)
            .MaximumLength(255);

        RuleFor(x => x.PublicationDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Publication date cannot be in the future");
    }
}
