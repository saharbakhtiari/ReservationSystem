using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{

    public interface IJwtGeneratorService
    {
        Task<string> GenerateJwtAsync(ApplicationUser user);
    }
}
