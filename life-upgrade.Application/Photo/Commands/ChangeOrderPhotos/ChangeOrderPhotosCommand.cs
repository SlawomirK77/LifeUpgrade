using MediatR;

namespace LifeUpgrade.Application.Photo.Commands.ChangeOrderPhotos;

public class ChangeOrderPhotosCommand : IRequest
{
    public List<string> Photos { get; set; } = default!;
}