using Domain.Contract.Enums;
using System;

namespace Application.Bookings.Queries.AdminGetBooking
{
    public class AdminGetBookingByIdDto
    {
        public long Id { get; set; }
        public AdminGetBookingByIdSpaceDto Space { get; set; } 
        public AdminGetBookingByIdProfileDto Profile { get; set; }
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
    public class AdminGetBookingByIdSpaceDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public SpaceType Type { get; set; }
    }
    public class AdminGetBookingByIdProfileDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
