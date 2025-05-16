using LifeUpgrade.Domain.Entities;

namespace LifeUpgrade.Domain.Interfaces;

public interface IPhotoRepository
{
    Task Create(Domain.Entities.Photo photo);
    Task<Domain.Entities.Photo?> GetByBytes(byte[] bytes);
    Task<IEnumerable<Domain.Entities.Photo>> GetPhotosByProductEncodedName(string encodedName);
}