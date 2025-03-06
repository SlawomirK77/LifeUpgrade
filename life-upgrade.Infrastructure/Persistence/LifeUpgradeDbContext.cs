using Microsoft.EntityFrameworkCore;

namespace LifeUpgrade.Infrastructure.Persistence;

public class LifeUpgradeDbContext : DbContext
{
    public LifeUpgradeDbContext(DbContextOptions<LifeUpgradeDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Domain.Entities.Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Product>()
            .OwnsOne(p => p.Details);
    }
}