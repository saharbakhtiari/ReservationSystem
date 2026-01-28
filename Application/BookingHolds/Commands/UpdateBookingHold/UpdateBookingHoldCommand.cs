using Application.BookingHolds.Commands.CreateBookingHold;
using Domain.Contract.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Application.BookingHolds.Commands.UpdateBookingHold
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingHoldManager)]
    public class UpdateBookingHoldCommand : IRequest
    {
        public long Id { get; set; }
        public long SpaceId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public BookingHoldStatus Status { get; set; }
    }

}
