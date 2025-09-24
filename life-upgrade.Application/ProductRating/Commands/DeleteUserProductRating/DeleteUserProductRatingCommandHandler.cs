using LifeUpgrade.Application.ApplicationUser;
using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.ProductRating.Commands.DeleteUserProductRating;

public class DeleteUserProductRatingCommandHandler : IRequestHandler<DeleteUserProductRatingCommand>
{
    private readonly IProductRatingRepository _productRatingRepository;
    private readonly IUserContext _userContext;

    public DeleteUserProductRatingCommandHandler(IProductRatingRepository productRatingRepository, IUserContext userContext)
    {
        _productRatingRepository = productRatingRepository;
        _userContext = userContext;
    }


    public async Task Handle(DeleteUserProductRatingCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        if (currentUser == null)
        {
            return;
        }

        await _productRatingRepository.DeleteUserRatingForProduct(currentUser.Id, request.ProductEncodedName);
    }
}