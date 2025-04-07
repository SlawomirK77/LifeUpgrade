using AutoMapper;
using LifeUpgrade.Application.Product;
using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Domain.Interfaces;

namespace LifeUpgrade.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task Create(ProductDto productDto)
    {
        var product = _mapper.Map<Domain.Entities.Product>(productDto);
        product.EncodeName();
        product.Details = new ProductDetails();
        await _productRepository.Create(product);
    }
}