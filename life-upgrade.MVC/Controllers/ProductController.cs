using LifeUpgrade.Application.Product;
using LifeUpgrade.Application.Product.Commands.CreateProduct;
using LifeUpgrade.Application.Product.Queries.GetAllProducts;
using LifeUpgrade.Application.Product.Queries.GetProductByEncodedName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        return RedirectToAction(nameof(Create));
    }
}