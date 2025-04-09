using LifeUpgrade.Domain.Entities;

namespace LifeUpgrade.Domain.Interfaces;

public interface IWebShopRepository
{
    Task Create(WebShop webShop);
    Task<IEnumerable<WebShop>> GetAllByEncodedName(string encodedName);
}