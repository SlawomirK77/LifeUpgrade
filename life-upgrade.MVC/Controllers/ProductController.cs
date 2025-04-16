using LifeUpgrade.Application.Product.Commands.CreateProduct;
using LifeUpgrade.Application.Product.Queries.GetAllProducts;
using LifeUpgrade.Application.Product.Queries.GetProductByEncodedName;
using LifeUpgrade.Application.WebShop.Commands;
using LifeUpgrade.Application.WebShop.Queries;
using LifeUpgrade.MVC.Extensions;
using LifeUpgrade.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        // await _mediator.Send(command);
        
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
}