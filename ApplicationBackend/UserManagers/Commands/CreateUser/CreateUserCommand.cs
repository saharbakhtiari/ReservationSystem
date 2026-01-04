using Application.UserManagers.Commands.CreateUser;
using AutoMapper;
using Domain.MemberProfiles;
using Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userid = await _userManager.CreateUserAsync(request.UserName?.Trim(), request.Password?.Trim(), request.FirstName?.Trim(), request.LastName?.Trim(), request.PhoneNumber.Trim(), request.EmployeeNumber?.Trim(), request.LoginProvider);
            var profile = _mapper.Map<MemberProfile>(request);
            profile.UserId = userid;
            await profile.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
