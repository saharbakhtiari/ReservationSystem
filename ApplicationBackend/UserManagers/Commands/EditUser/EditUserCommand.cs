using Application.UserManagers.Commands.EditUser;
using AutoMapper;
using Domain.Common;
using Domain.MemberProfiles;
using Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.UserManagers.Commands.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand>
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        public EditUserCommandHandler(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            await _userManager.UpdateUserAsync(_mapper.Map<UserInputDto>(request));
            var profile = await MemberProfile.GetProfileAsync(request.Id, cancellationToken);
            _mapper.Map(request, profile);
            await profile.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}