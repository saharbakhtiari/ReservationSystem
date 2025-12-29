using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Infrastructure.UserAccount
{
    public static class IdentityResultExtensions
    {
        //public static ServiceResult ToApplicationResult(this IdentityResult result)
        //{
        //    return result.Succeeded
        //        ? ServiceResult.Success()
        //        : ServiceResult.Failure(result.Errors.Select(e => e.Description));
        //}

        //public static ServiceResult<T> ToApplicationResult<T>(this IdentityResult result, T data = null) where T : class
        //{
        //    return result.Succeeded
        //        ? ServiceResult<T>.Success(data)
        //        : ServiceResult<T>.Failure(result.Errors.Select(e => e.Description), data);
        //}
    }
}