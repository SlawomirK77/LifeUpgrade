using System;
using AutoMapper;
using FluentAssertions;
using JetBrains.Annotations;
using LifeUpgrade.Application.Mappings;
using LifeUpgrade.Application.Product;
using LifeUpgrade.Domain.Entities;
using Xunit;

namespace life_upgrade.Application.Tests.Mappings;

[TestSubject(typeof(ProductMappingProfile))]
public class ProductMappingProfileTest
{

    [Fact]
    public void MappingProfile_ShouldMapProductDtoToProduct()
    {
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile(new ProductMappingProfile()));
        var mapper = configuration.CreateMapper();
        var dto = new ProductDto
        {
            Type = [ProductType.Electronics],
        };
        
        var result = mapper.Map<Product>(dto);
        
        result.Should().NotBeNull();
        result.Details.Should().NotBeNull();
        result.Details.Type.Should().Contain(dto.Type);
    }

    [Fact]
    public void MappingProfile_ShouldMapProductToProductDto()
    {
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile(new ProductMappingProfile()));
        var mapper = configuration.CreateMapper();
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Price = 100,
            Uri = new Uri("https://www.test.com"),
            Details = new ProductDetails
            {
                Type = [ProductType.Electronics, ProductType.Kitchen],
            }
        };
        
        var result = mapper.Map<ProductDto>(product);
        
        result.Should().NotBeNull();
        result.Name.Should().Be("Test");
        result.Price.Should().Be(100);
        result.Uri.Should().Be("https://www.test.com");
        result.Type.Should().Contain(product.Details.Type);
    }
}