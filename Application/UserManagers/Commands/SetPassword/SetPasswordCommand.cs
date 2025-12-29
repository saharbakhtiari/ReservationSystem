using Application.Common.Security;
using MediatR;

namespace Application.UserManagers.Commands.SetPassword
{
    [Authorize]
    public class SetPasswordCommand : IRequest
    {
        //public string UserName { get; set; }
        [RedactFromLogs]
        public string Password { get; set; }
    }
}