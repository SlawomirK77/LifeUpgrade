using LifeUpgrade.Domain.Entities;

namespace LifeUpgrade.Domain.Interfaces;

public interface IProductRepository
{
    Task Create(Domain.Entities.Product product);
    Task<Domain.Entities.Product?> GetByName(string name);
    Task<IQueryable<Domain.Entities.Product>> GetAllQueryable();
    Task<IEnumerable<Domain.Entities.Product>> GetAll();
    Task<Domain.Entities.Product?> GetByEncodedName(string encodedName);
    Task Commit();
}