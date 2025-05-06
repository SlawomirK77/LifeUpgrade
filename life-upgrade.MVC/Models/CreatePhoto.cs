namespace LifeUpgrade.MVC.Models;

public class CreatePhoto
{
    public string ProductEncodedName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public IFormFile ImageFile { get; set; } = default!;
}