using Application.UserManagers.Commands.VerifyRegisteration;
using AutoMapper;
using Domain.MemberProfiles;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.VerifyRegisteration
{
    public class VerifyRegisterationCommandHandler : IRequestHandler<VerifyRegisterationCommand, TokenDto>
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public VerifyRegisterationCommandHandler(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<TokenDto> Handle(VerifyRegisterationCommand request, CancellationToken cancellationToken)
        {
            await _userManager.VerifyOtpAsync(request.PhoneNumber.Trim(),request.VerifyCode, cancellationToken);
            var userid = await _userManager.RegisterUser(request.PhoneNumber, cancellationToken);
            var profile = _mapper.Map<MemberProfile>(request);
            profile.UserId = userid;
            await profile.SaveAsync(cancellationToken);
            await _userManager.AuthenticateAsync(request.PhoneNumber.Trim());
            return await _userManager.GetUserTokenIdAsync(request.PhoneNumber.Trim());
        }
    }
}
