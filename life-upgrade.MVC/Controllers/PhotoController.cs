using FluentValidation;
using LifeUpgrade.Application.Photo.Commands.CreatePhoto;
using LifeUpgrade.Application.Photo.Commands.DeletePhotos;
using LifeUpgrade.Application.Photo.Queries.GetPhotosByProductEncodedName;
using LifeUpgrade.Application.Services;
using LifeUpgrade.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LifeUpgrade.MVC.Controllers;

public class PhotoController : Controller
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreatePhotoCommand> _validator;
    private readonly FileService _fileService;

    public PhotoController(IMediator mediator, IValidator<CreatePhotoCommand> validator, FileService fileService)
    {
        _mediator = mediator;
        _validator = validator;
        _fileService = fileService;
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
    public async Task<IActionResult> Create(CreatePhotos photos)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _fileService.UploadImage(photos.ImageFiles, photos.ProductEncodedName);
        
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete(List<Guid> photoGuids)
    {
        await _mediator.Send(new DeletePhotosCommand(){PhotoIds = photoGuids});
        return Ok();
    }
}