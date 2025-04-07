using LifeUpgrade.Domain.Entities;

namespace LifeUpgrade.Domain.Interfaces;

public interface IProductRepository
{
    Task Create(Domain.Entities.Product product);
    Task<Product?> GetByName(string name);
}