using FluentValidation;
using LifeUpgrade.Domain.Interfaces;

namespace LifeUpgrade.Application.ProductRating.Commands.CreateProductRating;

public class CreateOrEditProductRatingCommandValidator : AbstractValidator<CreateOrEditProductRatingCommand>
{
    public CreateOrEditProductRatingCommandValidator(IProductRatingRepository repository)
    {
        RuleFor(command => command.Rating).NotNull();
        RuleFor(command => command.UserId).NotNull();
        RuleFor(command => command.ProductEncodedName).NotNull();
    }
}