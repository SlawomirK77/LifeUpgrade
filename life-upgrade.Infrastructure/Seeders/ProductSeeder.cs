using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LifeUpgrade.Infrastructure.Seeders;

public class ProductSeeder
{
    private readonly LifeUpgradeDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProductSeeder(LifeUpgradeDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
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

        await AddInitialUser();
    }

    private async Task AddInitialUser()
    {
        var user = new ApplicationUser
        {
            UserName = "test@test.com",
            Email = "test@test.com",
        };
        
        var password = new PasswordHasher<ApplicationUser>();
        var hashedPassword = password.HashPassword(user, "test");
        user.PasswordHash = hashedPassword;
        await _userManager.CreateAsync(user);
    }
}