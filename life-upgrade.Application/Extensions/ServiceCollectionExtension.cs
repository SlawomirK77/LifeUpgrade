using FluentValidation;
using FluentValidation.AspNetCore;
using LifeUpgrade.Application.Mappings;
using LifeUpgrade.Application.Product;
using LifeUpgrade.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LifeUpgrade.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();

        services.AddAutoMapper(typeof(ProductMappingProfile));

        services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>()
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
    }
}
