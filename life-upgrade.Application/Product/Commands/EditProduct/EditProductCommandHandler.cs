using AutoMapper;
using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.Product.Commands.EditProduct;

public class EditProductCommandHandler : IRequestHandler<EditProductCommand>
{
    private readonly IProductRepository _productRepository;

    public EditProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByEncodedName(request.EncodedName!);
        product.Name = request.Name;
        product.Price = request.Price;
        product.Uri = request.Uri;
        product.Details = new ProductDetails()
        {
            Type = request.Type,
        };
        
        product.EncodeName();

        await _productRepository.Commit();
    }
}