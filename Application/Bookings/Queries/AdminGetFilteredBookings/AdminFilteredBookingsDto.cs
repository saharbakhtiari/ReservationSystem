using Application.Bookings.Queries.GetBooking;
using Domain.Contract.Enums;
using System;

namespace Application.Bookings.Queries.AdminGetFilteredBookings
{
    public class AdminFilteredBookingsDto
    {
        public long Id { get; set; }
        public string SpaceTitle { get; set; }
        public string ProfileUserName { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public Currency Currency { get; set; }
        public string PriceSnapshot { get; set; }
        public string PolicySnapshot { get; set; }
        public DateTime ConfirmedAt { get; set; }
        public DateTime CancelledAt { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public DateTime SlotDate { get; set; }
    }
}
