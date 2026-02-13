using Application.BookingParticipants.Queries.GetFilteredBookingParticipants;
using Domain.Common;
using Domain.BookingParticipants;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingParticipants.Queries.GetFilteredSpace
{
    public class GetFilteredBookingParticipantsQueryHandler : IRequestHandler<GetFilteredBookingParticipantsQuery, PagedList<FilteredBookingParticipantsDto>>
    {
        public Task<PagedList<FilteredBookingParticipantsDto>> Handle(GetFilteredBookingParticipantsQuery request, CancellationToken cancellationToken)
        {
            return new BookingParticipant().Repository.GetFilteredAsync<FilteredBookingParticipantsDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
