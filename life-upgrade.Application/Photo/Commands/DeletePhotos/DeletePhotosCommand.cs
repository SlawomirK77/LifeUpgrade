using MediatR;

namespace LifeUpgrade.Application.Photo.Commands.DeletePhotos;

public class DeletePhotosCommand : IRequest
{
    public List<Guid> PhotoIds { get; set; } = default!;
}