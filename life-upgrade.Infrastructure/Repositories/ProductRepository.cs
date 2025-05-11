using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Domain.Interfaces;
using LifeUpgrade.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LifeUpgrade.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly LifeUpgradeDbContext _dbContext;

    public ProductRepository(LifeUpgradeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Create(Product product)
    {
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Product?> GetByName(string name)
        => _dbContext.Products.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());

    public async Task<IEnumerable<Product>> GetAll()
        => await _dbContext.Products.ToListAsync();

    public Task<Product?> GetByEncodedName(string encodedName)
        => _dbContext.Products.FirstOrDefaultAsync(p => p.EncodedName == encodedName);

    public Task Commit()
        => _dbContext.SaveChangesAsync();
}