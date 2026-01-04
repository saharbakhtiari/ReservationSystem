using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;

namespace Application.EducationCategories.Commands.PublishSlider
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_EducationPublishment)]
    public class PublishSliderCommand : IRequest
    {
        public long Id { get; set; }
        public bool IsPublished { get; set; }
    }
}
