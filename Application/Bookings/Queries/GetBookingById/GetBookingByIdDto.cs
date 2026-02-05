using Application.BookingHolds.Queries.GetBookingHold;
using Domain.Contract.Enums;
using System;

namespace Application.Bookings.Queries.GetBooking
{
    public class GetBookingByIdDto
    {
        public long Id { get; set; }
        public GetBookingByIdTimeSlotDto TimeSlot { get; set; }
        public GetBookingByIdProfileDto Profile { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public Currency Currency { get; set; }
        public string PriceSnapshot { get; set; }
        public string PolicySnapshot { get; set; }
        public DateTime ConfirmedAt { get; set; }
        public DateTime CancelledAt { get; set; }
    }
    public class GetBookingByIdSpaceDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public SpaceType Type { get; set; }
    }
    public class GetBookingByIdTariffDto
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public TariffUnit Unit { get; set; }
    }
    public class GetBookingByIdTimeSlotDto
    {
        public long Id { get; set; }
        public GetBookingByIdSpaceDto Space { get; set; }
        public GetBookingByIdTariffDto Tariff { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public DateTime SlotDate { get; set; }
        public TimeSlotType Type { get; set; }
    }
    public class GetBookingByIdProfileDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
