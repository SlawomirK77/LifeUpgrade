using MediatR;

namespace LifeUpgrade.Application.Product.Queries.GetAllProductsQueryable;

public class GetAllProductsQueryableQuery : IRequest<IQueryable<Domain.Entities.Product>>
{
    
}