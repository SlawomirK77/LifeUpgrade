using FluentValidation.TestHelper;
using LifeUpgrade.Application.WebShop.Commands;
using Xunit;

namespace life_upgrade.Application.Tests.WebShop.Commands;

public class CreateWebShopCommandValidatorTest
{
    [Fact]
    public void Validate_WithValidCommand_ShouldNotHaveValidationError()
    {
        var validator = new CreateWebShopCommandValidator();
        var command = new CreateWebShopCommand()
        {
            Name = "Name",
            Country = "Country",
            ProductEncodedName = "product1",
        };
        
        var result = validator.TestValidate(command);
        
        result.ShouldNotHaveAnyValidationErrors();
    }
    [Fact]
    public void Validate_WithInvalidCommand_ShouldHaveValidationError()
    {
        var validator = new CreateWebShopCommandValidator();
        var command = new CreateWebShopCommand()
        {
            Name = "",
            Country = "",
            ProductEncodedName = null,
        };
        
        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Country);
        result.ShouldHaveValidationErrorFor(c => c.ProductEncodedName);
    }
}