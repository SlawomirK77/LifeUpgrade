using AutoMapper;
using LifeUpgrade.Application.Photo.Commands;
using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.Photo.Queries.GetPhotosByProductEncodedName;

public class GetPhotosByProductEncodedNameQueryHandler : IRequestHandler<GetPhotosByProductEncodedNameQuery, IEnumerable<PhotoDto>>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IMapper _mapper;

    public GetPhotosByProductEncodedNameQueryHandler(IPhotoRepository photoRepository, IMapper mapper)
    {
        _photoRepository = photoRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<PhotoDto>> Handle(GetPhotosByProductEncodedNameQuery request, CancellationToken cancellationToken)
    {
        var photos = await _photoRepository.GetPhotosByProductEncodedName(request.EncodedName);
        var dtos = _mapper.Map<IEnumerable<PhotoDto>>(photos);
        
        return dtos;
    }
}