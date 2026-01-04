using MediatR;

namespace Application.Headers.Commands.DeleteHeader;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_HeaderManager)]
public class DeleteHeaderCommand : IRequest
{
    public long Id { get; set; }
}
