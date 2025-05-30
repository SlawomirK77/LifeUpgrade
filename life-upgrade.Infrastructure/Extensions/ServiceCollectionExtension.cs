using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Domain.Interfaces;
using LifeUpgrade.Infrastructure.Persistence;
using LifeUpgrade.Infrastructure.Repositories;
using LifeUpgrade.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LifeUpgrade.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LifeUpgradeDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("LifeUpgradeSqlServer")));

        services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<LifeUpgradeDbContext>();

        services.AddScoped<DatabaseSeeder>();
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IWebShopRepository, WebShopRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<IProductRatingRepository, ProductRatingRepository>();

        services.AddScoped<UserManager<ApplicationUser>>();
    }
}