using Domain.Contract.Enums;
using Domain.MemberProfiles;
using Domain.Spaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.Bookings.Commands.CreateBooking
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingManager)]
    public class CreateBookingCommand : IRequest<long>
    {
        public long SpaceId { get; set; } 
        public long TariffId { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public DateTime SlotDate { get; set; }
        public TimeSlotType Status { get; set; }
        public bool IsBooked { get; set; }
    }
}
