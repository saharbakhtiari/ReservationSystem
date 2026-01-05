using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Bookings
{
    public interface IBookingRepository : IGenericRepository<Booking, long>
    {
        Task<Booking> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
