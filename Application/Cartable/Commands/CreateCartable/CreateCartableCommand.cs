using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;

namespace Application.Cartable.Commands.CreateCartable
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BaseInformation)]
    public class CreateCartableCommand : IRequest<long>
    {
        public string Title { get; set; } = null!;


    }
}
