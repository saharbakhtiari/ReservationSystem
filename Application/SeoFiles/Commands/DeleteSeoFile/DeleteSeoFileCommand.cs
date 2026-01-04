using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using System;

namespace Application.SeoFiles.Commands.DeleteSeoFile;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_SeoManager)]
public class DeleteSeoFileCommand : IRequest
{
    public Guid FileGuid { get; set; }
}
