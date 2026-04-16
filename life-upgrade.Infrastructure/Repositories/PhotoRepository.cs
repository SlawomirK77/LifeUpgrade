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
        photo.Order = _dbContext.Photos.Max(x => x.Order) + 1;
        _dbContext.Photos.Add(photo);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Photo?> GetByBytes(List<byte> bytes)
        => await _dbContext.Photos.FirstOrDefaultAsync(x => x.Bytes == bytes);

    public Task<Photo?> GetById(Guid id)
        => _dbContext.Photos.FirstOrDefaultAsync(x => x.Id == id);

    public async Task DeleteByGuids(List<Guid> guids)
    {
        var productId = _dbContext.Photos.FirstOrDefault(x => x.Id == guids[0])?.ProductId;
        if (productId != null)
        {
            await _dbContext.Photos.Where(x => guids.Contains(x.Id)).ExecuteDeleteAsync();
            var dtos = await _dbContext.Photos.Where(x => x.ProductId == productId).OrderBy(x => x.Order).Select(x => new { x.Id, x.Order }).ToListAsync();
            var order = 0;

            foreach (var dto in dtos)
            {
                var photo = new Photo{ Id =  dto.Id, Order = order++ };
                _dbContext.Photos.Attach(photo);
                // photo.Order = order++;
            }
            await Commit(); 
        }
    }

    public Task Commit()
        => _dbContext.SaveChangesAsync();

    public async Task<IEnumerable<Photo>> GetPhotosByProductEncodedName(string encodedName)
    {
        var productId = _dbContext.Products.FirstOrDefaultAsync(x => x.EncodedName == encodedName).Result!.Id;
        
        var photos =  await _dbContext.Photos.Where(x => x.ProductId == productId).OrderBy(x => x.Order).ToListAsync();

        return photos;
    }

    public async Task<IEnumerable<Photo>> GetPhotosByOrderPosition(int orderPosition)
    {
        return await _dbContext.Photos.Where(x => x.Order == orderPosition).ToListAsync(); 
    }

    public async Task SetNewPhotosOrder1(List<Guid> guids)
    {
        var dtos = await _dbContext.Photos.Where(x => guids.Contains(x.Id)).Select(x => new { x.Id, x.Order }).ToListAsync();
        foreach (var dto in dtos)
        {
            var photo = new Photo { Id = dto.Id,  Order = dto.Order };
            _dbContext.Photos.Attach(photo);
            photo.Order = guids.IndexOf(dto.Id);
            _dbContext.Entry(photo).Property(p => p.Order).IsModified = true;
        }

        await _dbContext.SaveChangesAsync();
        // await Commit();
    }

    public async Task SetNewPhotosOrder(List<Guid> guids)
    {
        var dtos = await _dbContext.Photos.AsNoTracking().Where(x => guids.Contains(x.Id)).Select(x => new { x.Id, x.Order }).ToListAsync();
        foreach (var dto in dtos)
        {
            var photo = new Photo { Id = dto.Id,  Order = dto.Order };
            _dbContext.Photos.Attach(photo);
            photo.Order = guids.IndexOf(dto.Id);
            _dbContext.Entry(photo).Property("Order").IsModified = true;
        }

        await Commit();
    }
    
}