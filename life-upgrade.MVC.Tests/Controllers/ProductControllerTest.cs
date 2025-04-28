using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using LifeUpgrade.Application.Product;
using LifeUpgrade.Application.Product.Queries.GetAllProducts;
using LifeUpgrade.MVC.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace life_upgrade.MVC.Tests.Controllers;

[TestSubject(typeof(ProductController))]
public class ProductControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ProductControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task Index_ReturnsViewWithExpectedData_ForExistingProducts()
    {
        var products = new List<ProductDto>()
        {
            new ()
            {
                Name = "Product 1",
            },
            new ()
            {
                Name = "Product 2",
            },
            new ()
            {
                Name = "Product 3",
            }
        };
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);
        var client = _factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(service => service.AddScoped(_ => mediatorMock.Object)))
            .CreateClient();

        var response = await client.GetAsync($"/Product/Index");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Product 1")
            .And.Contain("Product 2")
            .And.Contain("Product 3");
    }
    
    [Fact]
    public async Task Index_ReturnsEmptyView_WhenNoProductExist()
    {
        var products = new List<ProductDto>();
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);
        var client = _factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(service => service.AddScoped(_ => mediatorMock.Object)))
            .CreateClient();

        var response = await client.GetAsync($"/Product/Index");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotContain("<h5 class=\"card-title\">");
    }
}