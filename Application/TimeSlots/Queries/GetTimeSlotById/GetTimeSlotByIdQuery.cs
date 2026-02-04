using MediatR;

namespace Application.TimeSlots.Queries.GetTimeSlot
{
    public class GetTimeSlotByIdQuery : IRequest<GetTimeSlotByIdDto>
    {
        public long Id { get; set; }
    }
}
