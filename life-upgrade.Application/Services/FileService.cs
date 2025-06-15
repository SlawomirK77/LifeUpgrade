using FluentValidation;
using LifeUpgrade.Application.Photo.Commands.CreatePhoto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LifeUpgrade.Application.Services;

public class FileService
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreatePhotoCommand> _validator;

    public FileService(IMediator mediator, IValidator<CreatePhotoCommand> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<IActionResult> UploadImage(IFormFile file, string productEncodedName, int existingPhotosCount, string? description = null)
    {
        await UploadFile(file, productEncodedName, existingPhotosCount, description);
        
        return new OkResult();
    }

    public async Task<IActionResult> UploadImage(IFormFileCollection files, string productEncodedName, int existingPhotosCount)
    {
        foreach (var file in files)
        {
            await UploadFile(file, productEncodedName, existingPhotosCount++);
        }

        return new OkResult();
    }

    private async Task<IActionResult> UploadFile(IFormFile file, string productEncodedName, int existingPhotosCount, string? description = null)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        if (memoryStream.Length is < 2000000 and > 0)
        {
            var command = new CreatePhotoCommand
            {
                Bytes = memoryStream.ToArray().ToList(),
                Description = description,
                FileExtension = file.FileName[(file.FileName.LastIndexOf('.') + 1)..],
                Size = memoryStream.Length,
                Order = existingPhotosCount,
                ProductEncodedName = productEncodedName,
            };
            var result = _validator.ValidateAsync(command).Result;

            if (!result.IsValid)
            {
                return new BadRequestResult();
            }
                
            await _mediator.Send(command);
        }
        else
        {
            return new BadRequestResult();
        }

        return new OkResult();
    }
}