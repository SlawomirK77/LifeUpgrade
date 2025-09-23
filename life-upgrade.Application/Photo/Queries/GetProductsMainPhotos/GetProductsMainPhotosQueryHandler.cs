using AutoMapper;
using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.Photo.Queries.GetProductsMainPhotos;

public class GetProductsMainPhotosQueryHandler : IRequestHandler<GetProductsMainPhotosQuery, IEnumerable<PhotoDto>>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IMapper _mapper;

    public GetProductsMainPhotosQueryHandler(IPhotoRepository  photoRepository, IMapper mapper)
    {
        _photoRepository = photoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PhotoDto>> Handle(GetProductsMainPhotosQuery request, CancellationToken cancellationToken)
    {
        var photos = await _photoRepository.GetPhotosByOrderPosition(0);
        var dtos = _mapper.Map<IEnumerable<PhotoDto>>(photos);
        
        return dtos;
    }
}