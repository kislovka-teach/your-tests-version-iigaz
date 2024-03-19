using FluentValidation;

namespace Variant1.Models.Validators;

public class DisplayValidator : AbstractValidator<Display>
{
    public DisplayValidator()
    {
        RuleFor(display => new { display.StartDate, display.EndDate })
            .Must(x => x.StartDate <= x.EndDate)
            .WithMessage("The start date cannot be later than the end date.");
    }
}