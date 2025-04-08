using Microsoft.EntityFrameworkCore;

namespace LifeUpgrade.Infrastructure.Persistence;

public class LifeUpgradeDbContext : DbContext
{
    public LifeUpgradeDbContext(DbContextOptions<LifeUpgradeDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Domain.Entities.Product> Products { get; set; }
    public DbSet<Domain.Entities.WebShop> WebShops { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Product>()
            .OwnsOne(p => p.Details);
        
        modelBuilder.Entity<Domain.Entities.Product>()
            .HasMany(p => p.WebShops)
            .WithOne(ws => ws.Product)
            .HasForeignKey(ws => ws.ProductId);
    }
}