using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Domain.Interfaces;
using LifeUpgrade.Infrastructure.Persistence;

namespace LifeUpgrade.Infrastructure.Repositories;

public class WebShopRepository : IWebShopRepository
{
    private readonly LifeUpgradeDbContext _dbContext;

    public WebShopRepository(LifeUpgradeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(WebShop webShop)
    {
        _dbContext.WebShops.Add(webShop);
        await _dbContext.SaveChangesAsync();
    }
}