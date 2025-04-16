using MediatR;

namespace LifeUpgrade.Application.WebShop.Queries;

public class GetProductWebShopsQuery : IRequest<IEnumerable<WebShopDto>>
{
    public string EncodedName { get; set; } = default!;
}