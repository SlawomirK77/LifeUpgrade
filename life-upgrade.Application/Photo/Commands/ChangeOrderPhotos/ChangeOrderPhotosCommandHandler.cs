using System.Text;
using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.Photo.Commands.ChangeOrderPhotos;

public class ChangeOrderPhotosCommandHandler : IRequestHandler<ChangeOrderPhotosCommand>
{
    private readonly IPhotoRepository _photoRepository;

    public ChangeOrderPhotosCommandHandler(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }

    public async Task Handle(ChangeOrderPhotosCommand request, CancellationToken cancellationToken)
    {
        await _photoRepository.SetNewPhotosOrder(request.PhotosGuids);
    }
}