namespace LifeUpgrade.Domain.Entities;

public class Photo
{
    public Guid Id { get; init; }
    public List<byte> Bytes { get; set; } = default!;
    public string? Description { get; set; }
    public string FileExtension { get; set; } = default!;
    public decimal Size { get; set; } = default!;
    public int Order { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public Product Product { get; set; } = default!;
}