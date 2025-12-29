using Application.UserManagers.Commands.RegisterUser;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUserManager _userManager;

        public RegisterUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await _userManager.SendRegisterOtpCodeAsync(request.PhoneNumber.Trim(), cancellationToken);
            return Unit.Value;
        }
    }
}
