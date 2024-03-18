using FluentValidation;

namespace Variant1.Models.Validators;

public class ReviewValidator : AbstractValidator<Review>
{
    public ReviewValidator()
    {
        RuleFor(review => review.Rating)
            .InclusiveBetween(Meta.ReviewRatingMin, Meta.ReviewRatingMax)
            .WithMessage(
                $"The rating should be between {Meta.ReviewRatingMin} and {Meta.ReviewRatingMax} (inclusive).");
    }
}