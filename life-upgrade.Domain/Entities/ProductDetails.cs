namespace LifeUpgrade.Domain.Entities
{
    public class ProductDetails
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }
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