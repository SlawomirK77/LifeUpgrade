using FluentValidation;
using LifeUpgrade.Application.Photo.Commands.CreatePhoto;

namespace LifeUpgrade.Application.Photo.Commands;

public class CreatePhotoCommandValidator : AbstractValidator<CreatePhotoCommand>
{
    public CreatePhotoCommandValidator()
    {
        RuleFor(x => x.Bytes).NotNull();
        RuleFor(x => x.FileExtension).NotEmpty();
        RuleFor(x => x.ProductEncodedName).NotEmpty().NotNull();
    }
}