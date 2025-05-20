using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using LifeUpgrade.Application.ApplicationUser;
using LifeUpgrade.Application.Mappings;
using LifeUpgrade.Application.Product.Commands.CreateProduct;
using LifeUpgrade.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LifeUpgrade.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<FileService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped(provider => new MapperConfiguration(cfg =>
        {
            var scope = provider.CreateScope();
            var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
            cfg.AddProfile(new ProductMappingProfile(userContext));
        }).CreateMapper());
        
        services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>()
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
    }
}
