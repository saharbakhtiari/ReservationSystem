using MediatR;
using System;

namespace Application.TimeSlots.Commands.UpdateTimeSlot
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_TimeSlotManager)]
    public class UpdateTimeSlotCommand : IRequest
    {
        public long Id { get; set; }
        public long SpaceId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }

}
