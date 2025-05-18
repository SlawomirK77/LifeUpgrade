namespace LifeUpgrade.Application.Photo.Commands;

public class PhotoDto
{
    public Guid Id { get; set; }
    public List<byte> Bytes { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string FileExtension { get; set; } = default!;
    public decimal Size { get; set; } = default!;
}