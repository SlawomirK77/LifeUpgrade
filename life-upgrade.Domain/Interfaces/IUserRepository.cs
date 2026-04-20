using LifeUpgrade.Domain.Entities;

namespace LifeUpgrade.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<ApplicationUser>> GetAll();
}