using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using LifeUpgrade.MVC.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace life_upgrade.MVC.Tests.Controllers;

[TestSubject(typeof(HomeController))]
public class HomeControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public HomeControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task About_ReturnViewWithRenderModel()
    {
        var client = _factory.CreateClient();
        
        var response = await client.GetAsync("/Home/About");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("<p>Use this page to detail your site's privacy policy.</p>")
            .And.Contain("<p>There is description.</p>");
    }
}