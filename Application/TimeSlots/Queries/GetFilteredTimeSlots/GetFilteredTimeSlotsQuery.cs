using Domain.Common;
using Domain.Contract.Enums;
using MediatR;
using System;

namespace Application.TimeSlots.Queries.GetFilteredTimeSlots
{
    public class GetFilteredTimeSlotsQuery : IRequest<PagedList<FilteredTimeSlotsDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public TimeSlotType Type { get; set; }
        public TimeSpan? StartAt { get; set; }
        public TimeSpan? EndAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
