using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LifeUpgrade.Infrastructure.Seeders;

public class DatabaseSeeder
{
    private readonly LifeUpgradeDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public DatabaseSeeder(LifeUpgradeDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task SeedAsync()
    {
        if (await _dbContext.Database.CanConnectAsync())
        {
            await SeedRoles();
            await SeedUsers();
            await SeedProducts();
            await SeedProductsRatings();
        }
    }

    private async Task SeedUsers()
    {
        if (_dbContext.Users.Any()) return;

        List<string> roles = ["Admin", "Moderator", "User"];

        foreach (var role in roles)
        {
            for (var i = 0; i < 2; i++)
            {
                var user = new ApplicationUser
                {
                    UserName = $"{role.ToLower()}{i}@mail.com",
                    Email = $"{role.ToLower()}{i}@mail.com",
                };
                var password = new PasswordHasher<ApplicationUser>();
                var hashedPassword = password.HashPassword(user, "admin");
                user.PasswordHash = hashedPassword;
                await _userManager.CreateAsync(user);
                
                await _userManager.AddToRolesAsync(user, GetRoles(role));
                
                // await _userManager.AddToRoleAsync(user,
                //     _dbContext.Roles.FirstAsync(x => x.Name == role).Result.Name!);
            }
        }
    }

    private async Task SeedProducts()
    {
        if (_dbContext.Products.Any()) return;
        
        var products = new List<Product>([ 
            new Product
            {
                Name = "Victorinox pikutek",
                Uri = new Uri("https://allegro.pl/listing?string=victorinox%20pikutek"),
                Price = 20,
                Details = new ProductDetails
                {
                    Type = [ProductType.Kitchen]
                },
            },
            new Product
            {
                Name = "Czytnik ebook amazon kindle paperwhite 5",
                Uri = new Uri("https://www.x-kom.pl/p/1297762-czytnik-ebook-amazon-kindle-paperwhite-16gb-7-2024-black.html"),
                Price = 889,
                Details = new ProductDetails
                {
                    Type = [ProductType.Electronics, ProductType.Health]
                },
            },
            new Product
            {
                Name = "Patelnia Florina Lava 30cm",
                Uri = new Uri("https://florina.pl/pl/products/patelnia-kamienna-florina-lava-stone-by-mateusz-gessler-30-cm-szara-6467.html"),
                Price = (decimal)199.99,
                Details = new ProductDetails
                {
                    Type = [ProductType.Kitchen]
                },
            },
            new Product
            {
                Name = "Fotel biurowy MARKUS",
                Uri = new Uri("https://www.ikea.com/pl/pl/p/markus-krzeslo-biurowe-vissle-ciemnoszary-70261150/"),
                Price = 599,
                Details = new ProductDetails
                {
                    Type = [ProductType.Health, ProductType.Other]
                },
            },
            new Product
            {
                Name = "Poduszka z łuską gryki",
                Uri = new Uri("https://allegro.pl/listing?string=poduszka%20z%20gryki"),
                Price = 70,
                Details = new ProductDetails
                {
                    Type = [ProductType.Health, ProductType.Other]
                },
            }
        ]);
        
        products.ForEach(p => p.EncodeName());
        _dbContext.Products.AddRange(products);
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

    private async Task SeedProductsRatings()
    {
        if (_dbContext.ProductRatings.Any()) return;
        var users = await _dbContext.Users.ToListAsync();
        var products = await _dbContext.Products.ToListAsync();
        List<ProductRating> ratings = [];
        var random = new Random();

        foreach (var user in users)
        {
            for (var i = 0; i < 3; i++)
            {
                ratings.Add(new ProductRating
                {
                    ProductEncodedName = products[random.Next(products.Count)].EncodedName,
                    UserId = user.Id,
                    Rating = random.Next(1, 5),
                });
            }
        }
        _dbContext.ProductRatings.AddRange(ratings);
        await _dbContext.SaveChangesAsync();
    }

    private static List<string> GetRoles(string role) => role switch
    {
        "Admin" => ["Admin", "Moderator", "User"],
        "Moderator" => ["Moderator", "User"],
        "User" => ["User"],
        _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
    };
}