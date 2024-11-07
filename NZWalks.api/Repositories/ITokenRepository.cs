using Microsoft.AspNetCore.Identity;

namespace NZWalks.api.Repositories
{
    public interface ITokenRepository
    {
       string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
