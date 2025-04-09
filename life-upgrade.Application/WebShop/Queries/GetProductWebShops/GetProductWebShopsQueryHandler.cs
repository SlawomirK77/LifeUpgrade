using AutoMapper;
using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.WebShop.Queries;

public class GetProductWebShopsQueryHandler : IRequestHandler<GetProductWebShopsQuery, IEnumerable<WebShopDto>>
{
    private readonly IWebShopRepository _webShopRepository;
    private readonly IMapper _mapper;

    public GetProductWebShopsQueryHandler(IWebShopRepository webShopRepository, IMapper mapper)
    {
        _webShopRepository = webShopRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<WebShopDto>> Handle(GetProductWebShopsQuery request, CancellationToken cancellationToken)
    {
        var result = await _webShopRepository.GetAllByEncodedName(request.EncodedName);
        var dtos = _mapper.Map<IEnumerable<WebShopDto>>(result);
        
        return dtos;
    }
}