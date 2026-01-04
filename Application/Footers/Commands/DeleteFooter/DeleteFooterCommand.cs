using MediatR;

namespace Application.Footers.Commands.DeleteFooter;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_FooterManager)]
public class DeleteFooterCommand : IRequest
{
    public long Id { get; set; }
}
