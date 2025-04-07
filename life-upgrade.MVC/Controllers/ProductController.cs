using LifeUpgrade.Application.Product;
using LifeUpgrade.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LifeUpgrade.MVC.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }
    
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAll();
        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(ProductDto product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }
        await _productService.Create(product);
        return RedirectToAction(nameof(Create));
    }
}