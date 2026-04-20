using MediatR;

namespace LifeUpgrade.Application.ApplicationUser.Queries;

public class GetAllUsersQuery : IRequest<IEnumerable<Domain.Entities.ApplicationUser>>
{
    
}