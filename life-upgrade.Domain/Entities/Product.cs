namespace LifeUpgrade.Domain.Entities;

public class Product
{
    public Guid Id { get; init; }
    public string Name { get; set; } = default!;
    public Uri Uri { get; set; } = default!;
    public decimal Price { get; set; }
    public ProductDetails Details { get; set; } = default!;
    public List<Photo> Photos { get; set; }
    public string EncodedName { get; private set; } = default!;

    public List<WebShop> WebShops { get; set; } = [];
    public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");
}