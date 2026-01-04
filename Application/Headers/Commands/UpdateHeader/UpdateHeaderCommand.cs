using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Headers.Commands.UpdateHeader
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_HeaderManager)]
    public class UpdateHeaderCommand : IRequest
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IFormFile? DataFiles { get; set; }
        public int Order { get; set; }
        public string Link { get; set; }
    }
}
