using Domain.Amenitys;
using Domain.Common;
using Domain.Contract.Enums;
using Domain.MemberProfiles;
using Domain.SpaceFiles;
using Domain.Spaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Bookings
{
    public class Booking : AuditableEntity
    {
        public Space Space { get; set; } = null!;
        public MemberProfile Profile { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public Currency Currency { get; set; }
        public string PriceSnapshot { get; set; }
        public string PolicySnapshot { get; set; }
        public DateTime ConfirmedAt { get; set; }
        public DateTime CancelledAt { get; set; }
        public bool IsDeleted { get; set; }

        public IBookingDomainService DomainService { get; set; }
        public IBookingRepository Repository { get; set; }

        public Booking()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<IBookingDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<IBookingRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<Booking> GetAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<IBookingRepository>();
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
