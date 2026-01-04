using Application.Cartable.Commands.RemoveUser;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Cartable.Commands.RemoveUser
{
    // [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_RuleManager_Create)]
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand>
    {
        private readonly IStringLocalizer _localizer;



        public RemoveUserCommandHandler(IStringLocalizer localizer)
        {
            _localizer = localizer;

        }


        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var cartable = await
                Domain.Cartables.Cartable.GetCartableByIdAsync(request.CartableId, cancellationToken) ??
                throw new UserFriendlyException(_localizer["Cartable not found"]);
            await cartable.DomainService.RemoveUser(request.UserId, cancellationToken);
            await cartable.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
