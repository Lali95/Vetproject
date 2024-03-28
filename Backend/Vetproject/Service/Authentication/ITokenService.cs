using Vetproject.Model;

namespace Vetproject.Service.Authentication;

public interface ITokenService
{
    string CreateToken(ApplicationUser user, string role);
}