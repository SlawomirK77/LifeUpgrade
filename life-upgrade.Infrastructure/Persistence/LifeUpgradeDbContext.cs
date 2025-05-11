using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LifeUpgrade.Infrastructure.Persistence;

public class LifeUpgradeDbContext : IdentityDbContext
{
    public LifeUpgradeDbContext(DbContextOptions<LifeUpgradeDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Domain.Entities.Product> Products { get; set; }
    public DbSet<Domain.Entities.WebShop> WebShops { get; set; }
    public DbSet<Domain.Entities.Photo> Photos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Domain.Entities.Product>()
            .OwnsOne(p => p.Details);
        
        modelBuilder.Entity<Domain.Entities.Product>()
            .HasMany(p => p.WebShops)
            .WithOne(ws => ws.Product)
            .HasForeignKey(ws => ws.ProductId);
        
        modelBuilder.Entity<Domain.Entities.Product>()
            .HasMany(p => p.Photos)
            .WithOne(ph => ph.Product)
            .HasForeignKey(ph => ph.ProductId);
    }
}