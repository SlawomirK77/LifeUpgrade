using System;
using System.Collections.Generic;
using FluentValidation.TestHelper;
using LifeUpgrade.Application.Photo.Commands.CreatePhoto;
using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Domain.Interfaces;
using Moq;
using Xunit;

namespace life_upgrade.Application.Tests.Photo.Commands;

public class CreatePhotoCommandValidatorTest
{
    private readonly Mock<IPhotoRepository> _mockPhotoRepository = new Mock<IPhotoRepository>();
    private static readonly List<byte> Bytes = [1, 2, 3, 4];
    private readonly LifeUpgrade.Domain.Entities.Photo _mockPhoto = new()
    {
        Id = Guid.Parse("c756af6b-c81b-45ea-94d1-720149740f2c"),
        Bytes = Bytes,
        Description = "description",
        FileExtension = "png",
        Size = Bytes.Count,
        ProductId = Guid.Parse("4b497f92-0c37-4a79-b357-b5513ac22013"),
        Product = new Product
        {
            Id = Guid.Parse("4b497f92-0c37-4a79-b357-b5513ac22013"),
            Name = "name",
            Uri = new Uri("http://www.test.com"),
            Price = 50,
            Details = new ProductDetails(),
            Photos = [],
            WebShops = [],
            ProductRatings = []
        }
    };
    
    [Fact]
    public void Validate_WithValidPhoto_ShouldNotHaveValidationError()
    {
        _mockPhotoRepository.Setup(repo => repo.GetByBytes(Bytes)).ReturnsAsync(_mockPhoto);
        var validator = new CreatePhotoCommandValidator(_mockPhotoRepository.Object);
        var command = new CreatePhotoCommand
        {
            Bytes = [..Bytes, 5],
            Description = "description",
            FileExtension = ".png",
            Size = Bytes.Count,
            ProductEncodedName = "encoded-name"
        };
        var result = validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public void Validate_WithValidPhoto_ShouldHaveValidationError_WhenPhotoAlreadyExists()
    {
        _mockPhotoRepository.Setup(repo => repo.GetByBytes(Bytes)).ReturnsAsync(_mockPhoto);
        var validator = new CreatePhotoCommandValidator(_mockPhotoRepository.Object);
        var command = new CreatePhotoCommand
        {
            Bytes = [..Bytes],
            Description = "description",
            FileExtension = ".png",
            Size = Bytes.Count,
            ProductEncodedName = "encoded-name"
        };
        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Bytes).WithErrorMessage("This photo already exists");
    }
}