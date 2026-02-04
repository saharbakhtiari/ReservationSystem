using Domain.Contract.Enums;
using MediatR;
using System;

namespace Application.TimeSlots.Commands.CreateTimeSlot
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_TimeSlotManager)]
    public class CreateTimeSlotCommand : IRequest
    {
        public long SpaceId { get; set; }
        public long TariffId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSlotType Type { get; set; }
        public int IntervalHours { get; set; }
    }
}
