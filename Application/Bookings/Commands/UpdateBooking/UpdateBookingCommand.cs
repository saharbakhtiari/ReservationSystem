using Application.Bookings.Commands.CreateBooking;
using Domain.Contract.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Application.Bookings.Commands.UpdateBooking
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_BookingManager)]
    public class UpdateBookingCommand : IRequest
    {
        public long Id { get; set; }
        public long TimeSlotId { get; set; }
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
