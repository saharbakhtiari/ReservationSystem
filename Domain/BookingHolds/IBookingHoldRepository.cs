using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.BookingHolds
{
    public interface IBookingHoldRepository : IGenericRepository<BookingHold, long>
    {
        Task<BookingHold> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
