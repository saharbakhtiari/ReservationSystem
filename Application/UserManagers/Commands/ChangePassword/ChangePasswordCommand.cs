using Application.Common.Security;
using MediatR;

namespace Application.UserManagers.Commands.ChangePassword
{
    [Authorize]
    public class ChangePasswordCommand : IRequest
    {
        [RedactFromLogs]
        public string CurrentPassword { get; set; }
        [RedactFromLogs]
        public string NewPassword { get; set; }
        [RedactFromLogs]
        public string ConfirmPassword { get; set; }
    }
}