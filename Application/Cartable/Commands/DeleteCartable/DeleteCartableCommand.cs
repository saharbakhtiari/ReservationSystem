using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;

namespace Application.Cartable.Commands.DeleteCartable;

[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BaseInformation)]
public class DeleteCartableCommand : IRequest
{
    public long Id { get; set; }
}
