using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;

namespace Application.Cartable.Commands.UpdateCartable
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BaseInformation)]
    public class UpdateCartableCommand : IRequest
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;

    }
}
