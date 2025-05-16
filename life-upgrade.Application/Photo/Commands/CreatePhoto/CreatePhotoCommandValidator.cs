using FluentValidation;
using LifeUpgrade.Application.Photo.Commands.CreatePhoto;
using LifeUpgrade.Domain.Interfaces;

namespace LifeUpgrade.Application.Photo.Commands.CreatePhoto;

public class CreatePhotoCommandValidator : AbstractValidator<CreatePhotoCommand>
{
    public CreatePhotoCommandValidator(IPhotoRepository photoRepository)
    {
        RuleFor(x => x.Bytes)
            .NotNull().WithMessage("There is no photo selected")
            .Custom((value, context) =>
            {
                var existingPhoto = photoRepository.GetByBytes(value).Result;
                if (existingPhoto != null)
                {
                    context.AddFailure("This photo already exists");
                }
            });
        RuleFor(x => x.FileExtension).NotEmpty();
        RuleFor(x => x.ProductEncodedName).NotEmpty().NotNull();
    }
}