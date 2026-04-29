using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.BookingHolds
{
    public interface IBookingHoldRepository : IGenericRepository<BookingHold, long>
    {
        Task<BookingHold> GetAsync(long id, CancellationToken cancellationToken);
        Task<BookingHold> GetExpiredAsync(CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, bool isAdmin, CancellationToken cancellationToken);
        Task<BookingHold> GetIncludedAsync(long id,bool isAdmin, CancellationToken cancellationToken);
    }
}
