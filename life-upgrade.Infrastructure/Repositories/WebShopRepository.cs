using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Domain.Interfaces;
using LifeUpgrade.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<WebShop>> GetAllByEncodedName(string encodedName)
        => await _dbContext.WebShops.Where(ws => ws.Product.EncodedName == encodedName).ToListAsync();
}