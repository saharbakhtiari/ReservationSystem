using MediatR;

namespace Application.Sliders.Commands.DeleteSlider;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_SliderManager)]
public class DeleteSliderCommand : IRequest
{
    public long Id { get; set; }
}
