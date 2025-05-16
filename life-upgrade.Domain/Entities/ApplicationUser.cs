using Microsoft.AspNetCore.Identity;

namespace LifeUpgrade.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        
    }
    public ApplicationUser(Guid id, string email, IEnumerable<string> roles)
    {
        Id = id;
        Email = email;
        Roles = roles;
    }

    public IEnumerable<string> Roles { get; set; } = [];
    [PersonalData]
    public byte[] Image { get; set; }
    public bool IsInRole(string role) => Roles.Contains(role);
}