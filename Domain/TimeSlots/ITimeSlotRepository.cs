using Domain.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.TimeSlots
{
    public interface ITimeSlotRepository : IGenericRepository<TimeSlot, long>
    {
        Task BulkInsertAsync(List<TimeSlot> timeSlots);
        Task<TimeSlot> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
        Task<TimeSlot> GetIncludedAsync(long id, CancellationToken cancellationToken);
    }
}
