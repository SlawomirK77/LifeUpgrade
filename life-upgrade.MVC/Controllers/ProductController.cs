using AutoMapper;
using LifeUpgrade.Application.Photo.Queries.GetPhotosByProductEncodedName;
using LifeUpgrade.Application.Photo.Queries.GetProductsMainPhotos;
using LifeUpgrade.Application.Product;
using LifeUpgrade.Application.Product.Commands.CreateProduct;
using LifeUpgrade.Application.Product.Commands.EditProduct;
using LifeUpgrade.Application.Product.Queries.GetAllProducts;
using LifeUpgrade.Application.Product.Queries.GetAllProductsQueryable;
using LifeUpgrade.Application.Product.Queries.GetProductByEncodedName;
using LifeUpgrade.Application.ProductRating.Commands.CreateProductRating;
using LifeUpgrade.Application.ProductRating.Queries.GetAllProductRatings;
using LifeUpgrade.Application.ProductRating.Queries.GetRatingsByProductEncodedName;
using LifeUpgrade.Application.WebShop.Commands.CreateWebShop;
using LifeUpgrade.Application.WebShop.Queries;
using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Domain.Interfaces;
using LifeUpgrade.MVC.Extensions;
using LifeUpgrade.MVC.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeUpgrade.MVC.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductController(ILogger<ProductController> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }
    
    public async Task<IActionResult> Index(int? pageNumber)
    {
        var products = await _mediator.Send(new GetAllProductsQueryableQuery());
        var photos = await _mediator.Send(new GetProductsMainPhotosQuery());
        
        var ratings = await _mediator.Send(new GetAllProductRatingsQuery());
        var ratingOrder = ratings.GroupBy(x => x.ProductEncodedName).Select(x => 
            new
            {
                name = x.Key,
                rating =  x.ToList().Average(y=>y.Rating)
            }).OrderByDescending(x => x.rating)
            .ToList();
        
        var orderedList = products.OrderByDescending(i => ratingOrder.Select(x => x.name).Reverse().ToList().IndexOf(i.EncodedName))
            .ThenBy(x => x.Price)
            .AsQueryable();
        var pageSize = 8;
        var viewData = await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize);
        foreach (var product in viewData)
        {
            var mainPhoto = photos.FirstOrDefault(x => x.ProductId == product.Id);
            if (mainPhoto != null)
            {
                product.Photos = new List<Photo>([_mapper.Map<Photo>(mainPhoto)]);    
            }
        }
        return View(viewData);
    }
    
    [Route("Product/{encodedName}/Details")]
    public async Task<IActionResult> Details(string encodedName)
    {
        var dto = await _mediator.Send(new GetProductByEncodedNameQuery(encodedName));
        dto.Photos = _mediator.Send(new GetPhotosByProductEncodedNameQuery() { EncodedName = encodedName }).Result.ToList();
        
        return View(dto);
    }
    
    [Route("Product/{encodedName}/Edit")]
    [Authorize(Roles = "User, Moderator")]
    public async Task<IActionResult> Edit(string encodedName)
    {
        var dto = await _mediator.Send(new GetProductByEncodedNameQuery(encodedName));

        if (!dto.IsEditable)
        {
            return RedirectToAction("NoAccess", "Home");
        }
        
        EditProductCommand model = _mapper.Map<EditProductCommand>(dto);
        
        return View(model);
    }
    
    [HttpPost]
    [Route("Product/{encodedName}/Edit")]
    [Authorize(Roles = "User, Moderator")]
    public async Task<IActionResult> Edit(string encodedName, EditProductCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }
        await _mediator.Send(command);
        
        this.SetNotification("success", $"Product: {command.Name} edited successfully");
        
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "User")]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [Authorize(Roles = "User")]
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
    [Authorize]
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

    [HttpGet]
    [Route("Product/{encodedName}/Rating")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetProductRatings(string encodedName)
    {
        var data = await _mediator.Send(new GetRatingsByProductEncodedNameQuery(){EncodedName = encodedName});
        return Ok(data);
    }
    
    [HttpPost]
    [Route("Product/Rating")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> CreateRating(CreateOrEditProductRatingCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _mediator.Send(command);
        return Ok();
    }
}