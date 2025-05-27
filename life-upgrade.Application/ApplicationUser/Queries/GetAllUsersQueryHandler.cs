using LifeUpgrade.Domain.Interfaces;
using MediatR;

namespace LifeUpgrade.Application.ApplicationUser.Queries;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery ,IEnumerable<Domain.Entities.ApplicationUser>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<Domain.Entities.ApplicationUser>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users= await _userRepository.GetAll();
        
        return users;
    }
}