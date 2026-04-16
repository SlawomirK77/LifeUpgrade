namespace LifeUpgrade.Domain.Entities;

public class WebShop
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
    public string Country { get; set; } = default!;

    public Guid ProductId { get; set; } = default!;
    public virtual Product Product { get; set; } = default!;
}