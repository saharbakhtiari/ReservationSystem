using Application.UserManagers.Commands.AssignRole;
using Application.UserManagers.Commands.ChangePassword;
using Application.UserManagers.Commands.CreateUser;
using Application.UserManagers.Commands.DeleteUser;
using Application.UserManagers.Commands.EditPhoneNumber;
using Application.UserManagers.Commands.EditRegisteredUser;
using Application.UserManagers.Commands.EditUser;
using Application.UserManagers.Commands.LoginUser;
using Application.UserManagers.Commands.OtpUser;
using Application.UserManagers.Commands.RegisterUser;
using Application.UserManagers.Commands.SetPassword;
using Application.UserManagers.Commands.VerifyOtpLogin;
using Application.UserManagers.Commands.VerifyRegisteration;
using Application.UserManagers.Queries.GetPermission;
using Application.UserManagers.Queries.GetSuggestUsers;
using Application.UserManagers.Queries.GetUserRoles;
using Application.UserManagers.Queries.GetUsers;
using Application.UserManagers.Queries.HasPassword;
using Application.UserManagers.Queries.IsLogin;
using Application_Backend.Common;
using DNTCaptcha.Core;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using WebAppBlazor.Server.Controllers.V1;
using Application.UserManagers.Commands.LogoutUser;

namespace WebAppBlazor.Server.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var user = await Mediator.SendWithUow(new GetCurrentUserInfoQuery() { });
            return Ok(user);
        }
        /// <summary>
        /// ایجاد یک کاربر توسط کسی که مجوز مدیریت کاربران را دارد
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpPost("editprofile")]
        [ValidateDNTCaptcha]
        public async Task<IActionResult> UpdateProfileUser([FromForm] EditProfileUserCommand dto)
        {
            if (!ModelState.IsValid)
                throw new UserFriendlyException("کد امنیتی به درستی وارد نشده است");
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpPut("editphone")]
        public async Task<IActionResult> UpdatePhone([FromBody] EditPhoneNumberCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var dto = new DeleteUserCommand() { Id = id };
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpPost("login")]
        [ValidateDNTCaptcha]
        public async Task<IActionResult> LoginUser([FromForm] LoginUserCommand dto)
        {
            if (!ModelState.IsValid)
                throw new UserFriendlyException("کد امنیتی به درستی وارد نشده است");
            var output = await Mediator.SendWithUow(dto);
            Response.Cookies.Append("X-Access-Token", output.access_token, new CookieOptions() { HttpOnly = true, MaxAge = TimeSpan.FromHours(1), SameSite = SameSiteMode.None, Secure = true, IsEssential = true });


            return Ok();
        }
        [HttpGet("logOut")]
        public async Task<IActionResult> logOut()
        {
            Response.Cookies.Append("X-Access-Token", "", new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Expires = DateTime.Now, Secure = true });
            LogoutUserCommand command = new();
            await Mediator.SendWithUow(command);
            return Ok();
        }
        /// <summary>
        /// درخواست ورود با موبایل
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("otplogin")]
        [ValidateDNTCaptcha]
        public async Task<IActionResult> LoginUser([FromForm] OtpLoginUserCommand dto)
        {
            if (!ModelState.IsValid)
                throw new UserFriendlyException("کد امنیتی به درستی وارد نشده است");

            var output = await Mediator.SendWithUow(dto);
            return Ok(output);
        }
        /// <summary>
        /// تایید ورود از طریق موبایل با احراز کد 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("verifyotplogin")]
        public async Task<IActionResult> LoginUser([FromBody] VerifyOtpLoginCommand dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Cookies.Append("X-Access-Token", output.access_token, new CookieOptions() { HttpOnly = true, MaxAge = TimeSpan.FromHours(1), SameSite = SameSiteMode.None, Secure = true });
            return Ok();
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetPermissions()
        {
            var output = await Mediator.SendWithUow(new GetPermissionQuery());
            return Ok(output);
        }

        [HttpGet("islogin")]
        public async Task<IActionResult> IsLogin()
        {
            var output = await Mediator.SendWithUow(new IsLoginQuery());
            return Ok(output);
        }

        [HttpGet("haspassword")]
        public async Task<IActionResult> HasPassword()
        {
            var output = await Mediator.SendWithUow(new HasPasswordQuery());
            return Ok(output);
        }

        [HttpGet("version")]
        //[RateLimit("EndpointRateLimitPolicy")]
        public IActionResult GetVersion()
        {
            Version version = typeof(Program).Assembly.GetName().Version;
            return Ok(version.ToString());
        }

        [HttpGet("users/{pageNumber:int}/{pageSize:int}/{sort}/{filter?}")]
        public async Task<IActionResult> GetUsers(int pageNumber, int pageSize, string sort, string filter = null)
        {
            var output = await Mediator.SendWithUow(new GetUsersQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Filter = filter,
                Sort = sort
            });
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }

        [HttpGet("roles/{userId:Guid}")]
        public async Task<IActionResult> GetRoles(Guid userId)
        {
            var output = await Mediator.SendWithUow(new GetUserRolesQuery()
            {
                UserId = userId
            });
            return Ok(output);
        }

        [HttpGet("suggestusers/{filter?}")]
        public async Task<IActionResult> GetSuggestUsers(string filter = null)
        {
            var output = await Mediator.SendWithUow(new GetSuggestUsersQuery()
            {
                Filter = filter ?? String.Empty
            });
            return Ok(output);
        }



        [HttpPost("changepassword")]
        [ValidateDNTCaptcha]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordCommand dto)
        {
            if (!ModelState.IsValid)
                throw new UserFriendlyException("کد امنیتی به درستی وارد نشده است");
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpPost("setpassword")]
        [ValidateDNTCaptcha]
        public async Task<IActionResult> SetPassword([FromForm] SetPasswordCommand dto)
        {
            if (!ModelState.IsValid)
                throw new UserFriendlyException("کد امنیتی به درستی وارد نشده است");
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }
        /// <summary>
        /// بررسی می کند اگر شماره موبایل ارسالی متعلق به کاربری نباشد  برای او یک کد یک بار مصرف می فرستد
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("sendOtp")]
        [ValidateDNTCaptcha]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserCommand dto)
        {
            if (!ModelState.IsValid)
                throw new UserFriendlyException("کد امنیتی به درستی وارد نشده است");
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }
        /// <summary>
        /// تایید عملیات ثبت نام توسط موبایل با ورود کد 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyRegisterationUser([FromBody] VerifyRegisterationCommand dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Cookies.Append("X-Access-Token", output.access_token, new CookieOptions() { HttpOnly = true, MaxAge = TimeSpan.FromHours(1), SameSite = SameSiteMode.None, Secure = true });
            return Ok();
        }
    }
}
