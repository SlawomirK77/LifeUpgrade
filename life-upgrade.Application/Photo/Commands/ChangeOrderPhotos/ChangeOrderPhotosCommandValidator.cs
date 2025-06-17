using FluentValidation;

namespace LifeUpgrade.Application.Photo.Commands.ChangeOrderPhotos;

public class ChangeOrderPhotosCommandValidator : AbstractValidator<ChangeOrderPhotosCommand>
{
    public ChangeOrderPhotosCommandValidator()
    {
        RuleFor(x => x.PhotosGuids).NotEmpty().NotNull();
    }
}