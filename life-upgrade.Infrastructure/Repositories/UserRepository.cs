using LifeUpgrade.Domain.Entities;
using LifeUpgrade.Domain.Interfaces;
using LifeUpgrade.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LifeUpgrade.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LifeUpgradeDbContext _dbContext;

    public UserRepository(LifeUpgradeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<ApplicationUser>> GetAll()
    {
        return await _dbContext.Users.ToListAsync();
    }
}