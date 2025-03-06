namespace LifeUpgrade.Domain.Entities
{
    public class ProductDetails
    {
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
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