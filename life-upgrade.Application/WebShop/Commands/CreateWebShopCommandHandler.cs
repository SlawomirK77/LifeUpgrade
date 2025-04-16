using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.WebShop.Commands;

public class CreateWebShopCommandHandler : IRequestHandler<CreateWebShopCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IWebShopRepository _webShopRepository;

    public CreateWebShopCommandHandler(IProductRepository productRepository, IWebShopRepository webShopRepository)
    {
        _productRepository = productRepository;
        _webShopRepository = webShopRepository;
    }
    public async Task Handle(CreateWebShopCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByEncodedName(request.ProductEncodedName);

        var webShop = new Domain.Entities.WebShop()
        {
            Name = request.Name,
            Country = request.Country,
            ProductId = product.Id,
        };
        
        await _webShopRepository.Create(webShop);
    }
}