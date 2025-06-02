using AutoMapper;
using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.Product.Queries.GetAllProductsQueryable;

public class GetAllProductsQueryableQueryHandler : IRequestHandler<GetAllProductsQueryableQuery, IQueryable<Domain.Entities.Product>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryableQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public Task<IQueryable<Domain.Entities.Product>> Handle(GetAllProductsQueryableQuery request, CancellationToken cancellationToken)
    {
        var products = _productRepository.GetAllQueryable();
        return products;
    }
}