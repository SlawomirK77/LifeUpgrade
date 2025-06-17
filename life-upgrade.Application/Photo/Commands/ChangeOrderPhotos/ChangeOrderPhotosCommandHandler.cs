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
        var newOrder = 0;

        foreach (var photoId in request.PhotosGuids)
        {
            var savedPhoto = await _photoRepository.GetById(photoId);
            savedPhoto!.Order = newOrder++;
            await _photoRepository.Commit();
        }
    }
}