using FluentValidation;
using LifeUpgrade.Application.Photo.Commands.CreatePhoto;
using LifeUpgrade.Application.Photo.Queries.GetPhotosByProductEncodedName;
using LifeUpgrade.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LifeUpgrade.MVC.Controllers;

public class PhotoController : Controller
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreatePhotoCommand> _validator;

    public PhotoController(IMediator mediator, IValidator<CreatePhotoCommand> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpGet]
    [Route("Product/{encodedName}/Photo")]
    public async Task<IActionResult> Get(string encodedName)
    {
        var data = await _mediator.Send(new GetPhotosByProductEncodedNameQuery(){EncodedName = encodedName });
        return Ok(data);
    }

    [HttpPost]
    [Authorize]
    [Route("Product/Photo")]
    public async Task<IActionResult> Create(CreatePhoto photo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using (var memoryStream = new MemoryStream())
        {
            await photo.ImageFile.CopyToAsync(memoryStream);

            if (memoryStream.Length is < 2000000 and > 0)
            {
                var command = new CreatePhotoCommand
                {
                    Bytes = memoryStream.ToArray(),
                    Description = photo.Description,
                    FileExtension = photo.ImageFile.FileName[(photo.ImageFile.FileName.LastIndexOf('.') + 1)..],
                    Size = memoryStream.Length,
                    ProductEncodedName = photo.ProductEncodedName,
                };
                var result = await _validator.ValidateAsync(command);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }
                
                await _mediator.Send(command);
            }
            else return BadRequest();
        }
        return Ok();
    }
}