using Application.UserManagers.Commands.LoginUser;
using Application.UserManagers.Commands.OtpUser;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.LoginUser
{
    public class OtpLoginUserCommandHandler : IRequestHandler<OtpLoginUserCommand>
    {
        private readonly IUserManager _userManager;

        public OtpLoginUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(OtpLoginUserCommand request, CancellationToken cancellationToken)
        {
            await _userManager.SendLoginOtpCodeAsync(request.PhoneNumber.Trim(), cancellationToken);
            return Unit.Value;
        }
    }
}