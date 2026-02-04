using Domain.Common;
using MediatR;

namespace Application.TimeSlots.Queries.GetFilteredTimeSlots
{
    public class GetFilteredTimeSlotsQuery : IRequest<PagedList<FilteredTimeSlotsDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
