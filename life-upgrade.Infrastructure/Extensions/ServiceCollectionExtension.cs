using LifeUpgrade.Domain.Interfaces;
using LifeUpgrade.Infrastructure.Persistence;
using LifeUpgrade.Infrastructure.Repositories;
using LifeUpgrade.Infrastructure.Seeders;
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

        services.AddScoped<ProductSeeder>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}