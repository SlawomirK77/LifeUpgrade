using LifeUpgrade.Application.Photo.Commands;
using MediatR;

namespace LifeUpgrade.Application.Photo.Queries.GetPhotosByProductEncodedName;

public class GetPhotosByProductEncodedNameQuery : IRequest<IEnumerable<PhotoDto>>
{
    public string EncodedName { get; set; } = default!;
}