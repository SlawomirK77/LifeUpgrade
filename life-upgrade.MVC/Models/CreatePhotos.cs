namespace LifeUpgrade.MVC.Models;

public class CreatePhotos
{
    public string ProductEncodedName { get; set; } = default!;
    public IFormFileCollection ImageFiles { get; set; } = default!;
    
}