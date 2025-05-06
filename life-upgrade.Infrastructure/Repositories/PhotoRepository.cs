using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Domain.Interfaces;
using LifeUpgrade.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LifeUpgrade.Infrastructure.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly LifeUpgradeDbContext _dbContext;

    public PhotoRepository(LifeUpgradeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Create(Photo photo)
    {
        _dbContext.Photos.Add(photo);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Photo>> GetPhotosByProductEncodedName(string encodedName)
    {
        var productId = _dbContext.Products.FirstOrDefaultAsync(x => x.EncodedName == encodedName).Result!.Id;
        
        return await _dbContext.Photos.Where(x => x.ProductId == productId).ToListAsync();
    }
}