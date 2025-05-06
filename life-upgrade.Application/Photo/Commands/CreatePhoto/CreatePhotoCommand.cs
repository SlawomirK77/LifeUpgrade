using MediatR;
using Microsoft.AspNetCore.Http;

namespace LifeUpgrade.Application.Photo.Commands.CreatePhoto;

public class CreatePhotoCommand : PhotoDto, IRequest
{
    public string ProductEncodedName { get; set; } = default!;
}