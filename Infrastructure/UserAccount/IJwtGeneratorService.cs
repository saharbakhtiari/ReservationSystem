using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{

    public interface IJwtGeneratorService
    {
        Task<string> GenerateJwtAsync(ApplicationUser user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
