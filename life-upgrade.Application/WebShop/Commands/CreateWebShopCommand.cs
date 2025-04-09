using MediatR;

namespace LifeUpgrade.Application.WebShop.Commands;

public class CreateWebShopCommand : WebShopDto, IRequest
{
    public string ProductEncodedName { get; set; } = default!;
}