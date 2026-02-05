using Domain.Contract.Enums;
using Domain.MemberProfiles;
using Domain.Spaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.BookingHolds.Commands.CreateBookingHold
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingHoldManager)]
    public class CreateBookingHoldCommand : IRequest<long>
    {
        public long TimeSlotId { get; set; }
        public string Token { get; set; }
        public BookingHoldStatus Status { get; set; }
    }
}
