using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using System;

namespace Application.Cartable.Commands.AddUser
{
    [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BaseInformation)]
    public class AddUserCommand : IRequest
    {
        public long CartableId { get; set; }
        public Guid UserId { get; set; }

    }
}
