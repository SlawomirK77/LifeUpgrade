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

    public async Task<Photo?> GetByBytes(List<byte> bytes)
        => await _dbContext.Photos.FirstOrDefaultAsync(x => x.Bytes == bytes);

    public Task<Photo?> GetById(Guid id)
        => _dbContext.Photos.FirstOrDefaultAsync(x => x.Id == id);

    public async Task DeleteByGuids(List<Guid> guids)
    {
        var x =  await _dbContext.Photos.Where(x => guids.Contains(x.Id)).ExecuteDeleteAsync();
    }

    public Task Commit()
        => _dbContext.SaveChangesAsync();

    public async Task<IEnumerable<Photo>> GetPhotosByProductEncodedName(string encodedName)
    {
        var productId = _dbContext.Products.FirstOrDefaultAsync(x => x.EncodedName == encodedName).Result!.Id;
        
        return await _dbContext.Photos.Where(x => x.ProductId == productId).OrderBy(x => x.Order).ToListAsync();
    }

    public async Task<IEnumerable<Photo>> GetPhotosByOrderPosition(int orderPosition)
    {
        return await _dbContext.Photos.Where(x => x.Order == orderPosition).ToListAsync(); 
    }
}