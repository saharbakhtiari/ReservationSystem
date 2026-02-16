using Domain.Contract.Enums;
using System;
using System.Collections.Generic;

namespace Application.AdminBookingHolds.Queries.GetAdminBookingHold
{
    public class AdminGetBookingHoldByIdDto
    {
        public long Id { get; set; }
        public AdminGetBookingHoldByIdProfileDto Profile { get; set; }
        public List<AdminGetBookingHoldByIdTimeSlotDto> TimeSlots { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public BookingHoldStatus Status { get; set; }
    }
    public class AdminGetBookingHoldByIdSpaceDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public SpaceType Type { get; set; }
    }
    public class AdminGetBookingHoldByIdTariffDto
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public TariffUnit Unit { get; set; }
    }
    public class AdminGetBookingHoldByIdTimeSlotDto
    {
        public long Id { get; set; }
        public AdminGetBookingHoldByIdSpaceDto Space { get; set; }
        public AdminGetBookingHoldByIdTariffDto Tariff { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public DateTime SlotDate { get; set; }
        public TimeSlotType Type { get; set; }
        public int Count { get; set; }
    }
    public class AdminGetBookingHoldByIdProfileDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
