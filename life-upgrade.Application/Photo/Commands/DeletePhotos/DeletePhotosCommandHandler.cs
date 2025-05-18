using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.Photo.Commands.DeletePhotos;

public class DeletePhotosCommandHandler : IRequestHandler<DeletePhotosCommand>
{
    private readonly IPhotoRepository _photoRepository;

    public DeletePhotosCommandHandler(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }
    public async Task Handle(DeletePhotosCommand request, CancellationToken cancellationToken)
    {
         await _photoRepository.DeleteByGuids(request.PhotoIds);
    }
}