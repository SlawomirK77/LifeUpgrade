using MediatR;

namespace LifeUpgrade.Application.ProductRating.Queries.GetRatingsByProductEncodedName;

public class GetRatingsByProductEncodedNameQuery : IRequest<IEnumerable<ProductRatingDto>>
{
    public string EncodedName { get; set; } = default!;
}