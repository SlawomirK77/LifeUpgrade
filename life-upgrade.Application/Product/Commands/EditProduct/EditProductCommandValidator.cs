using FluentValidation;
using LifeUpgrade.Domain.Interfaces;

namespace LifeUpgrade.Application.Product.Commands.EditProduct;

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator(IProductRepository repository)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(2).WithMessage("Name must be between 2 and 50 characters")
            .MaximumLength(50).WithMessage("Name must be between 2 and 50 characters")
            .Custom((value, context) =>
            {
                var existingProduct = repository.GetByName(value).Result;
                if (existingProduct != null && existingProduct.Name != value)
                {
                    context.AddFailure($"{value} is not unique name");
                }
            });
        RuleFor(x => x.Uri)
            .NotEmpty().WithMessage("Price cannot be empty");
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price cannot be empty");
    }
}