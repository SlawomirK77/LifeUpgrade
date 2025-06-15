using MediatR;

namespace LifeUpgrade.Application.Photo.Commands.CreatePhoto;

public class CreatePhotoCommand : PhotoDto, IRequest
{
    public string ProductEncodedName { get; set; } = default!;
    public int Order { get; set; }
}