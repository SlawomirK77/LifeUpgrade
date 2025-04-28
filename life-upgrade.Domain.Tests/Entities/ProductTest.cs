using System;
using FluentAssertions;
using JetBrains.Annotations;
using LifeUpgrade.Domain.Entities;
using Xunit;

namespace life_upgrade.Domain.Tests.Entities;

[TestSubject(typeof(Product))]
public class ProductTest
{

    [Fact]
    public void EncodeName_ShouldSetEncodedName()
    {
        var product = new Product();
        product.Name = "Test Product";
        
        product.EncodeName();
        
        product.EncodedName.Should().Be("test-product");
    }

    [Fact]
    public void EncodeName_ShouldThrowException_WhenNameIsNull()
    {
        var product = new Product();
        
        Action action = () => product.EncodeName();

        action.Invoking(a => a.Invoke()).Should().Throw<NullReferenceException>();
    }
}