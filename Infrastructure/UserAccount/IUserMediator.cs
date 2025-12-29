using Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{
    public interface IUserMediator
    {
        Task<List<UserModel>> SearchUsers(string searchFilter, bool exactly = false, int maxLoop = 10);
        Task<bool> ValidateUser(string username, string password);
        Task<UserModel> GetUser(string username);
    }
}