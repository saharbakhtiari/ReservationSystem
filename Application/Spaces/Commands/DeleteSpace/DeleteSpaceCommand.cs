using MediatR;

namespace Application.Spaces.Commands.DeleteSpace;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_SpaceManager)]
public class DeleteSpaceCommand : IRequest
{
    public long Id { get; set; }
}
