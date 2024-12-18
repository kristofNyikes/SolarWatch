using Microsoft.AspNetCore.Identity;

namespace SolarWatchApi.Services.Authentication;

public interface ITokenService
{
    public string CreateToken(IdentityUser user, string role);
}