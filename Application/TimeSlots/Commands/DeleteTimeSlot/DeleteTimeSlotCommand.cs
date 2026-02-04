using MediatR;

namespace Application.TimeSlots.Commands.DeleteTimeSlot;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_TimeSlotManager)]
public class DeleteTimeSlotCommand : IRequest
{
    public long Id { get; set; }
}
