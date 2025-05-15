using AutoMapper;
using LifeUpgrade.Application.ProductRating.Queries.GetRatingsByProductEncodedName;
using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.ProductRating.Queries;

public class GetRatingsByProductEncodedNameQueryHandler : IRequestHandler<GetRatingsByProductEncodedNameQuery, IEnumerable<ProductRatingDto>>
{
    private readonly IProductRatingRepository _productRatingRepository;
    private readonly IMapper _mapper;

    public GetRatingsByProductEncodedNameQueryHandler(IProductRatingRepository productRatingRepository, IMapper mapper)
    {
        _productRatingRepository = productRatingRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProductRatingDto>> Handle(GetRatingsByProductEncodedNameQuery request, CancellationToken cancellationToken)
    {
        var ratings = await _productRatingRepository.GetByEncodedName(request.EncodedName);
        var dtos = _mapper.Map<IEnumerable<ProductRatingDto>>(ratings);
        
        return dtos;
    }
}