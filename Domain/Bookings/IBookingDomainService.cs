using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Bookings
{
    public interface IBookingDomainService : IBaseDomainService<Booking>
    {
        Task SetProfile(CancellationToken cancellationToken);
        Task SetTimeSlot(long slotId, CancellationToken cancellationToken);
    }
}
