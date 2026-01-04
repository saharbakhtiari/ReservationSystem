using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;

namespace Application.SeoFiles.Commands.StoreSeoFile
{
   // [Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_SeoManager)]
    public class StoreSeoFileCommand : IRequest<StoreSeoFileDto>
    {
        public string Name { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public byte[] DataFiles { get; set; } = null!;
    }
}
