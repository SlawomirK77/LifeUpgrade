using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.Photo.Commands.CreatePhoto;

public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IPhotoRepository _photoRepository;

    public CreatePhotoCommandHandler(IProductRepository productRepository, IPhotoRepository photoRepository)
    {
        _productRepository = productRepository;
        _photoRepository = photoRepository;
    }
    public async Task Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByEncodedName(request.ProductEncodedName);
        var photoOrder = request.Order;
        
        var photo = new Domain.Entities.Photo
        {
            Bytes = request.Bytes,
            Description = request.Description,
            FileExtension = request.FileExtension,
            Size = request.Size,
            ProductId = product!.Id,
            Order = photoOrder,
        };
        
        await _photoRepository.Create(photo);
    }
}