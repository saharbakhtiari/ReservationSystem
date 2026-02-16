using Application.BookingHolds.Queries.GetFilteredBookingHolds;
using Domain.Common;
using Domain.BookingHolds;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingHolds.Queries.GetFilteredSpace
{
    public class GetFilteredBookingHoldsQueryHandler : IRequestHandler<GetFilteredBookingHoldsQuery, PagedList<FilteredBookingHoldsDto>>
    {
        public Task<PagedList<FilteredBookingHoldsDto>> Handle(GetFilteredBookingHoldsQuery request, CancellationToken cancellationToken)
        {
            return new BookingHold().Repository.GetFilteredAsync<FilteredBookingHoldsDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize,false, cancellationToken);
        }
    }
}
