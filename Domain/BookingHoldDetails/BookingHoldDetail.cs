using Domain.BookingHolds;
using Domain.Common;
using Domain.Contract.Enums;
using Domain.MemberProfiles;
using Domain.Spaces;
using Domain.TimeSlots;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.BookingHoldDetails
{
    public class BookingHoldDetail : AuditableEntity
    {
        public TimeSlot TimeSlot { get; set; } = null!;

        [JsonIgnore]
        public BookingHold BookingHold { get; set; }
        public int Count { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public IBookingHoldDetailDomainService DomainService { get; set; }
        [JsonIgnore]
        public IBookingHoldDetailRepository Repository { get; set; }

        public BookingHoldDetail()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<IBookingHoldDetailDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<IBookingHoldDetailRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }
    }
}
