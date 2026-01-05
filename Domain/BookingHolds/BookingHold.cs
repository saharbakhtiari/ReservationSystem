using Domain.Common;
using Domain.Contract.Enums;
using Domain.MemberProfiles;
using Domain.Spaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.BookingHolds
{
    public class BookingHold : AuditableEntity
    {
        public Space Space { get; set; } = null!;
        public MemberProfile Profile { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public bool IsDeleted { get; set; }

        public IBookingHoldDomainService DomainService { get; set; }
        public IBookingHoldRepository Repository { get; set; }

        public BookingHold()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<IBookingHoldDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<IBookingHoldRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<BookingHold> GetAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<IBookingHoldRepository>();
            var item = await repository.GetAsync(id, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
    }
}
