using MediatR;

namespace LifeUpgrade.Application.ProductRating.Queries.GetAllProductRatings;

public class GetAllProductRatingsQuery : IRequest<IEnumerable<ProductRatingDto>>
{
    
}