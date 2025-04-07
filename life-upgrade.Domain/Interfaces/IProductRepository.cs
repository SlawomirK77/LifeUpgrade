using LifeUpgrade.Domain.Entities;

namespace LifeUpgrade.Domain.Interfaces;

public interface IProductRepository
{
    Task Create(Domain.Entities.Product product);
    Task<Domain.Entities.Product?> GetByName(string name);
    Task<IEnumerable<Domain.Entities.Product>> GetAll();
}