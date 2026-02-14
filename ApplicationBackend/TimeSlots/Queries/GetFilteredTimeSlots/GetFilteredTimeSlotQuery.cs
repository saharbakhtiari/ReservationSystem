using Application.TimeSlots.Queries.GetFilteredTimeSlots;
using Domain.Common;
using Domain.Contract.Enums;
using Domain.TimeSlots;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.TimeSlots.Queries.GetFilteredSpace
{
    public class GetFilteredTimeSlotsQueryHandler : IRequestHandler<GetFilteredTimeSlotsQuery, PagedList<FilteredTimeSlotsDto>>
    {
        public Task<PagedList<FilteredTimeSlotsDto>> Handle(GetFilteredTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            return new TimeSlot().Repository.GetFilteredAsync<FilteredTimeSlotsDto>(request.Filter, 
                                                                                    request.Sort,
                                                                                    request.StartDate,
                                                                                    request.EndDate,
                                                                                    request.Type,
                                                                                    request.StartAt,
                                                                                    request.EndAt,
                                                                                    request.PageNumber,
                                                                                    request.PageSize,
                                                                                    cancellationToken);
        }
    }
}
