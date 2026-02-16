using Application.BookingHolds.Queries.AdminGetFilteredBookingHolds;
using Domain.Common;
using Domain.BookingHolds;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingHolds.Queries.AdminGetFilteredSpace
{
    public class AdminGetFilteredBookingHoldsQueryHandler : IRequestHandler<AdminGetFilteredBookingHoldsQuery, PagedList<AdminFilteredBookingHoldsDto>>
    {
        public Task<PagedList<AdminFilteredBookingHoldsDto>> Handle(AdminGetFilteredBookingHoldsQuery request, CancellationToken cancellationToken)
        {
            return new BookingHold().Repository.GetFilteredAsync<AdminFilteredBookingHoldsDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize,true, cancellationToken);
        }
    }
}
