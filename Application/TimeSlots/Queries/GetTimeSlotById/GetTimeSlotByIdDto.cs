using Domain.Contract.Enums;
using System;

namespace Application.TimeSlots.Queries.GetTimeSlot
{
    public class GetTimeSlotByIdDto
    {
        public long Id { get; set; }
        public GetTimeSlotByIdSpaceDto Space { get; set; } 
        public GetTimeSlotByIdProfileDto Profile { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public TimeSlotType Status { get; set; }
    }
    public class GetTimeSlotByIdSpaceDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public SpaceType Type { get; set; }
    }
    public class GetTimeSlotByIdProfileDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
