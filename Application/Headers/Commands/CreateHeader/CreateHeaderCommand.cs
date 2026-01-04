using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Headers.Commands.CreateHeader
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_HeaderManager)]
    public class CreateHeaderCommand : IRequest<long>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public IFormFile DataFiles { get; set; }
        public int Order { get; set; }
        public string Link { get; set; }
    }
}
