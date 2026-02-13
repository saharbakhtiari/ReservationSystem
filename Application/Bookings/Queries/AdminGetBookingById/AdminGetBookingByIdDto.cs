using Application.BookingHolds.Queries.GetBookingHold;
using Application.Bookings.Queries.GetBooking;
using Domain.Contract.Enums;
using System;
using System.Collections.Generic;

namespace Application.Bookings.Queries.AdminGetBooking
{
    public class AdminGetBookingByIdDto
    {
        public long Id { get; set; }
        public List<AdminGetBookingByIdTimeSlotDto> TimeSlots { get; set; }
        public List<AdminGetBookingByIdParticipantDto> Participants { get; set; }
        public AdminGetBookingByIdProfileDto Profile { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public Currency Currency { get; set; }
       // public string PriceSnapshot { get; set; }
       // public string PolicySnapshot { get; set; }
        public DateTime ConfirmedAt { get; set; }
        public DateTime CancelledAt { get; set; }
    }
    public class AdminGetBookingByIdSpaceDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public SpaceType Type { get; set; }
    }
    public class AdminGetBookingByIdTariffDto
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public TariffUnit Unit { get; set; }
    }
    public class AdminGetBookingByIdTimeSlotDto
    {
        public long Id { get; set; }
        public AdminGetBookingByIdSpaceDto Space { get; set; }
        public AdminGetBookingByIdTariffDto Tariff { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public DateTime SlotDate { get; set; }
        public TimeSlotType Type { get; set; }
        public int Count { get; set; }
    }
    public class AdminGetBookingByIdProfileDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class AdminGetBookingByIdParticipantDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
