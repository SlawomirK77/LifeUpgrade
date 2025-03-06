using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Infrastructure.Persistence;

namespace LifeUpgrade.Infrastructure.Seeders;

public class ProductSeeder
{
    private readonly LifeUpgradeDbContext _dbContext;
    public ProductSeeder(LifeUpgradeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if (await _dbContext.Database.CanConnectAsync())
        {
            if (!_dbContext.Products.Any())
            {
                var product = new Domain.Entities.Product
                {
                    Name = "Victorinox pikutek",
                    Uri = new Uri("https://allegro.pl/listing?string=victorinox%20pikutek"),
                    Price = 20,
                    Details = new ProductDetails
                    {
                        Type = [ProductType.Kitchen]
                    }
                };
                product.EncodeName();
                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}