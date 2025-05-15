using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Domain.Interfaces;
using LifeUpgrade.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LifeUpgrade.Infrastructure.Repositories;

public class ProductRatingRepository : IProductRatingRepository
{
    private readonly LifeUpgradeDbContext _dbContext;

    public ProductRatingRepository(LifeUpgradeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Create(ProductRating rating)
    {
        _dbContext.ProductRatings.Add(rating);
        await _dbContext.SaveChangesAsync();
    }
    public async Task Commit()
    => await _dbContext.SaveChangesAsync();

    public async Task<IEnumerable<ProductRating>> GetByEncodedName(string encodedName)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.EncodedName == encodedName);
        var ratings = await _dbContext.ProductRatings.Where(pr => pr.ProductEncodedName == product!.EncodedName).ToListAsync();
        
        return ratings;
    }

    public Task<ProductRating?> GetByEncodedNameAndUserId(string encodedName, Guid userId)
    {
        var result = _dbContext.ProductRatings.FirstOrDefaultAsync(pr => pr.ProductEncodedName == encodedName && pr.UserId == userId);
        
        return result;
    }
}