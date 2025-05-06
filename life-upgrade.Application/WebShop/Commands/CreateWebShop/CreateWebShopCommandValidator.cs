using FluentValidation;

namespace LifeUpgrade.Application.WebShop.Commands.CreateWebShop;

public class CreateWebShopCommandValidator : AbstractValidator<CreateWebShopCommand>
{
    public CreateWebShopCommandValidator()
    {
        RuleFor(ws => ws.Name).NotEmpty();
        RuleFor(ws => ws.Country).NotEmpty().NotNull();
        RuleFor(ws => ws.ProductEncodedName).NotEmpty().NotNull();
    }
}