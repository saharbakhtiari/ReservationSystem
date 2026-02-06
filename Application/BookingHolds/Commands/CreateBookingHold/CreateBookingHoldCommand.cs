using Domain.Contract.Enums;
using MediatR;
using System.Collections.Generic;

namespace Application.BookingHolds.Commands.CreateBookingHold
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingHoldManager)]
    public class CreateBookingHoldCommand : IRequest<long>
    {
        public List<CreateBookingHoldDetail> Details { get; set; }
        public string Token { get; set; }
        public BookingHoldStatus Status { get; set; }
    }
    public class CreateBookingHoldDetail
    {
        public long TimeSlotId { get; set; }
        public int Count { get; set; }
    }
}
