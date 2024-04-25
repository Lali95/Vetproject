using Microsoft.AspNetCore.Identity;

namespace Vetproject.Service.Authentication;

public interface ITokenService
{
    public string CreateToken(IdentityUser user);
}
