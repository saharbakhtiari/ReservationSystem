using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.BookingHolds
{
    public interface IBookingHoldDomainService : IBaseDomainService<BookingHold>
    {
        Task SetExpired(CancellationToken cancellationToken);
        Task SetProfile(CancellationToken cancellationToken);
        Task SetTimeSlot(long slotId, int count, CancellationToken cancellationToken);
    }
}
