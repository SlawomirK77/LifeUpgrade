namespace LifeUpgrade.Application.ProductRating;

public class ProductRatingDto
{
    public string ProductEncodedName { get; set; } = default!;
    public Guid UserId { get; set; }
    public int Rating { get; set; }
}