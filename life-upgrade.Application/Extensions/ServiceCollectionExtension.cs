using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using LifeUpgrade.Application.Mappings;
using LifeUpgrade.Application.Product;
using Microsoft.Extensions.DependencyInjection;

namespace LifeUpgrade.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(typeof(ProductMappingProfile));

        services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>()
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
    }
}
