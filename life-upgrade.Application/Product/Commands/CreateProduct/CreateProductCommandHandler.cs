using AutoMapper;
using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.Product.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Domain.Entities.Product>(request);
        product.EncodeName();
        product.Details = new Domain.Entities.ProductDetails();
        await _productRepository.Create(product);
    }
}