using MediatR;

namespace LifeUpgrade.Application.Product.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
{
    
}