using AutoMapper;
using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.ProductRating.Queries.GetAllProductRatings;

public class GetAllProductRatingsQueryHandler : IRequestHandler<GetAllProductRatingsQuery, IEnumerable<ProductRatingDto>>
{
    private readonly IProductRatingRepository _productRatingRepository;
    private readonly IMapper _mapper;

    public GetAllProductRatingsQueryHandler(IProductRatingRepository productRatingRepository, IMapper mapper)
    {
        _productRatingRepository = productRatingRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProductRatingDto>> Handle(GetAllProductRatingsQuery request, CancellationToken cancellationToken)
    {
        var ratings = await _productRatingRepository.GetAll();
        var dtos = _mapper.Map<IEnumerable<ProductRatingDto>>(ratings);
        
        return dtos;
    }
}