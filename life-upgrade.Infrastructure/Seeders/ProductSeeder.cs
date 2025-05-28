using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            await SeedRoles();
            await SeedUser();
            await SeedProduct();
        }
    }

    private async Task SeedUser()
    {
        if (_dbContext.Users.Any()) return;
        
        var user = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin",
        };
        var password = new PasswordHasher<ApplicationUser>();
        var hashedPassword = password.HashPassword(user, "admin");
        user.PasswordHash = hashedPassword;
        await _userManager.CreateAsync(user);
        await _userManager.AddToRoleAsync(user, _dbContext.Roles.FirstAsync(x => x.Name == "Admin").Result.Name!);
    }

    private async Task SeedProduct()
    {
        if (_dbContext.Products.Any()) return;
        
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

    private async Task SeedRoles()
    {
        if (_dbContext.Roles.Any()) return;
        var roles = new List<IdentityRole<Guid>>()
        {
            new ()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new ()
            {
                Name = "Moderator",
                NormalizedName = "MODERATOR"
            },
            new ()
            {
                Name = "User",
                NormalizedName = "USER"
            }
        };
        _dbContext.Roles.AddRange(roles);
        await _dbContext.SaveChangesAsync();
    }
}