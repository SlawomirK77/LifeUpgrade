namespace LifeUpgrade.MVC.Models;

public class CreatePhoto
{
    public string ProductEncodedName { get; set; } = default!;
    public string? Description { get; set; }
    public IFormFile ImageFile { get; set; } = default!;
}