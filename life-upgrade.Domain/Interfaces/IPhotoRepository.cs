using LifeUpgrade.Domain.Entities;

namespace LifeUpgrade.Domain.Interfaces;

public interface IPhotoRepository
{
    Task Commit();
    Task Create(Domain.Entities.Photo photo);
    Task<Domain.Entities.Photo?> GetByBytes(List<byte> bytes);
    Task DeleteByGuids(List<Guid> guids);
    Task<IEnumerable<Domain.Entities.Photo>> GetPhotosByProductEncodedName(string encodedName);
}