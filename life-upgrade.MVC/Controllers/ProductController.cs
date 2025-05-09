using LifeUpgrade.Application.Photo.Commands.CreatePhoto;
using LifeUpgrade.Application.Photo.Queries.GetPhotosByProductEncodedName;
using LifeUpgrade.Application.Product.Commands.CreateProduct;
using LifeUpgrade.Application.Product.Queries.GetAllProducts;
using LifeUpgrade.Application.Product.Queries.GetProductByEncodedName;
using LifeUpgrade.Application.WebShop.Commands.CreateWebShop;
using LifeUpgrade.Application.WebShop.Queries;
using LifeUpgrade.MVC.Extensions;
using LifeUpgrade.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace LifeUpgrade.MVC.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;

    public ProductController(ILogger<ProductController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    public async Task<IActionResult> Index()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return View(products);
    }
    
    [Route("Product/{encodedName}/Details")]
    public async Task<IActionResult> Details(string encodedName)
    {
        var dto = await _mediator.Send(new GetProductByEncodedNameQuery(encodedName));
        return View(dto);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }
        await _mediator.Send(command);
        
        this.SetNotification("success", $"Product: {command.Name} created successfully");
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [Route("Product/WebShop")]
    public async Task<IActionResult> CreateWebShop(CreateWebShopCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _mediator.Send(command);
        
        return Ok();
    }

    [HttpGet]
    [Route("Product/{encodedName}/WebShop")]
    public async Task<IActionResult> GetProductWebShops(string encodedName)
    {
        var data = await _mediator.Send(new GetProductWebShopsQuery(){EncodedName = encodedName});
        return Ok(data);
    }

    [HttpPost]
    [Route("Product/Photo")]
    public async Task<IActionResult> CreatePhoto(CreatePhoto photo)
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
                await _mediator.Send(new CreatePhotoCommand
                {
                    Bytes = memoryStream.ToArray(),
                    Description = photo.Description,
                    FileExtension = photo.ImageFile.FileName[(photo.ImageFile.FileName.LastIndexOf('.') + 1)..],
                    Size = memoryStream.Length,
                    ProductEncodedName = photo.ProductEncodedName,
                });
            }
            else return BadRequest();
        }
        return Ok();
    }

    [HttpGet]
    [Route("Product/{encodedName}/Photo")]
    public async Task<IActionResult> GetProductPhotos(string encodedName)
    {
        var data = await _mediator.Send(new GetPhotosByProductEncodedNameQuery(){EncodedName = encodedName });
        var photos =  new FileStreamResult(new MemoryStream(data.First().Bytes), new MediaTypeHeaderValue("application/octet-stream"));        
        return Ok(data);
    }
}