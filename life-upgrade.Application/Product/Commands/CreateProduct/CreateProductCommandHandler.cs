using AutoMapper;
using LifeUpgrade.Application.ApplicationUser;
using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.Product.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUserContext userContext)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _userContext = userContext;
    }
    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        if (currentUser == null || !currentUser.IsInRole("User"))
        {
            return;
        }
        var product = _mapper.Map<Domain.Entities.Product>(request);
        product.EncodeName();
        product.Details = new Domain.Entities.ProductDetails
        {
            CreatedById = currentUser.Id,
            Type = request.Type
        };

        await _productRepository.Create(product);
    }
}