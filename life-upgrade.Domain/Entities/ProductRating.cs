namespace LifeUpgrade.Domain.Entities;

public class ProductRating
{
    public Guid Id { get; set; }
    public string ProductEncodedName { get; set; } = default!;
    public Guid UserId { get; set; }
    public int Rating { get; set; }
}