using MediatR;
using System.Collections.Generic;

namespace Application.Footers.Commands.UpdateFooter
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_FooterManager)]
    public class UpdateFooterCommand : IRequest
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Order { get; set; }
        public List<UpdateFooterLinkCommand> Link { get; set; }
    }
}
