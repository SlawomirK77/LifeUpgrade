using AutoMapper;
using LifeUpgrade.Application.ApplicationUser;
using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.ProductRating.Commands.CreateProductRating;

public class CreateOrEditProductRatingCommandHandler : IRequestHandler<CreateOrEditProductRatingCommand>
{
    private readonly IProductRatingRepository _productRatingRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public CreateOrEditProductRatingCommandHandler(IProductRatingRepository productRatingRepository, IMapper mapper, IUserContext userContext)
    {
        _productRatingRepository = productRatingRepository;
        _mapper = mapper;
        _userContext = userContext;
    }
    public async Task Handle(CreateOrEditProductRatingCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        if (currentUser == null)
        {
            return;
        }

        var currentUserProductRating = await _productRatingRepository.GetByEncodedNameAndUserId(request.ProductEncodedName, currentUser.Id);
        if (currentUserProductRating != null)
        {
            currentUserProductRating.Rating = request.Rating;
            await _productRatingRepository.Commit();
            return;
        }
        
        var productRating = _mapper.Map<Domain.Entities.ProductRating>(request);
        productRating.UserId = currentUser.Id;
        await _productRatingRepository.Create(productRating);
    }
}