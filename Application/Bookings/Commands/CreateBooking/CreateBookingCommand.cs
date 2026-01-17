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
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public Currency Currency { get; set; }
        public string PriceSnapshot { get; set; }
        public string PolicySnapshot { get; set; }
        public DateTime ConfirmedAt { get; set; }
        public DateTime CancelledAt { get; set; }
    }
}
