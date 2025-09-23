using MediatR;

namespace LifeUpgrade.Application.Photo.Commands.ChangeOrderPhotos;

public class ChangeOrderPhotosCommand : IRequest
{
    public List<Guid> PhotosGuids { get; set; } = default!;
}