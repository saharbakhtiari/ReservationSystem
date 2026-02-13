using Application.BookingParticipants.Queries.AdminGetFilteredBookingParticipants;
using Domain.Common;
using Domain.BookingParticipants;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingParticipants.Queries.GetFilteredSpace
{
    public class AdminGetFilteredBookingParticipantsQueryHandler : IRequestHandler<AdminGetFilteredBookingParticipantsQuery, PagedList<AdminFilteredBookingParticipantsDto>>
    {
        public Task<PagedList<AdminFilteredBookingParticipantsDto>> Handle(AdminGetFilteredBookingParticipantsQuery request, CancellationToken cancellationToken)
        {
            return new BookingParticipant().Repository.GetFilteredAsync<AdminFilteredBookingParticipantsDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
