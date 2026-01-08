using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.BookingParticipants
{
    public interface IBookingParticipantRepository : IGenericRepository<BookingParticipant, long>
    {
        Task<BookingParticipant> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
