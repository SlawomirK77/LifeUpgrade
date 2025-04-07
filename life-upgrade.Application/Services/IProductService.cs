using LifeUpgrade.Application.Product;
using LifeUpgrade.Domain.Entities;

namespace LifeUpgrade.Application.Services;

public interface IProductService
{
    Task Create(ProductDto product);
    Task<IEnumerable<ProductDto>> GetAll();
}