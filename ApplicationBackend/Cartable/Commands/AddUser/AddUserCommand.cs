using Application.Cartable.Commands.AddUser;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Cartable.Commands.AddUser
{
    // [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_RuleManager_Create)]
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
    {
        private readonly IStringLocalizer _localizer;



        public AddUserCommandHandler(IStringLocalizer localizer)
        {
            _localizer = localizer;

        }


        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var cartable = await
                Domain.Cartables.Cartable.GetCartableByIdAsync(request.CartableId, cancellationToken) ??
                throw new UserFriendlyException(_localizer["Cartable not found"]);
            await cartable.DomainService.AddUser(request.UserId, cancellationToken);
            await cartable.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
