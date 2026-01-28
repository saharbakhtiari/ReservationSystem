using Domain.Contract.Enums;
using MediatR;
using System;

namespace Application.BookingHolds.Commands.UpdateBookingHoldStatus
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingHoldManager)]
    public class UpdateBookingHoldStatusCommand : IRequest
    {
        public long Id { get; set; }
        public BookingHoldStatus Status { get; set; }
    }

}
