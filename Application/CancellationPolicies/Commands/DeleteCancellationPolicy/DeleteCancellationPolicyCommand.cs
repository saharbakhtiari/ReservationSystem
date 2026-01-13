using MediatR;

namespace Application.CancellationPolicys.Commands.DeleteCancellationPolicy;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_CancellationPolicyManager)]
public class DeleteCancellationPolicyCommand : IRequest
{
    public long Id { get; set; }
}
