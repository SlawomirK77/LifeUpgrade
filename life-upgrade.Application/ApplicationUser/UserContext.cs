using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace LifeUpgrade.Application.ApplicationUser
{
    public interface IUserContext
    {
        CurrentUser GetCurrentUser();
    }



    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public CurrentUser GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("There is no context user present");
            }

            var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;

            return new CurrentUser(id, email);
        }
    }
}