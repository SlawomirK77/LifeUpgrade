using Microsoft.AspNetCore.Identity;

namespace LifeUpgrade.Domain.Entities
{
    public class ProductDetails
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }
        public List<ProductType> Type { get; set; } = [];
    }

    public enum ProductType
    {
        Food,
        Kitchen,
        Sport,
        Health,
        Electronics,
        Other
    }

}