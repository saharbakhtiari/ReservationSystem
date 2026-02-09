using Domain.Bookings;
using Domain.Common;
using Domain.TimeSlots;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.BookingDetails
{
    public class BookingDetail : AuditableEntity
    {
        public TimeSlot TimeSlot { get; set; } = null!;
        public Booking Booking { get; set; }
        public int Count { get; set; }
        public bool IsDeleted { get; set; }

        public IBookingDetailDomainService DomainService { get; set; }
        public IBookingDetailRepository Repository { get; set; }

        public BookingDetail()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<IBookingDetailDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<IBookingDetailRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }
    }
}
