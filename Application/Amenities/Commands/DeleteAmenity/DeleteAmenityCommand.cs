using MediatR;

namespace Application.Amenitys.Commands.DeleteAmenity;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_AmenityManager)]
public class DeleteAmenityCommand : IRequest
{
    public long Id { get; set; }
}
