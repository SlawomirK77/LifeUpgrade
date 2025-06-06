using LifeUpgrade.Application.Photo;

namespace LifeUpgrade.MVC.Models
{

    public class EditPhotos
    {
        public string ProductEncodedName { get; set; } = default!;
        public IEnumerable<EditPhoto> Photos { get; set; } = [];

        public EditPhotos(string productEncodedName, IEnumerable<PhotoDto> photos)
        {
            ProductEncodedName = productEncodedName;
            Photos = photos.Select(x => new EditPhoto()
            {
                Id = x.Id,
                Bytes = x.Bytes,
                Description = x.Description,
            }).ToList();
        }
    }

    public class EditPhoto
    {
        public Guid Id { get; set; }
        public List<byte> Bytes { get; set; } = default!;
        public string Description { get; set; } = default!;
    } 
}