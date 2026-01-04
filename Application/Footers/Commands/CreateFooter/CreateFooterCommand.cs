using MediatR;
using System.Collections.Generic;

namespace Application.Footers.Commands.CreateFooter
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_FooterManager)]
    public class CreateFooterCommand : IRequest<long>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int Order { get; set; }
        public List<CreateFooterLinkCommand> Links { get; set; }
    }
}
