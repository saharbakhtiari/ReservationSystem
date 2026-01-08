using Domain.Amenitys;
using Domain.Bookings;
using Domain.Common;
using Domain.Contract.Enums;
using Domain.MemberProfiles;
using Domain.SpaceFiles;
using Domain.Spaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Payments
{
    public class Payment : AuditableEntity
    {
        public Booking Booking { get; set; }
        public decimal Amount { get; set; }
        public string GateWay { get; set; }
        public PaymentStatus Status { get; set; }
        public long TrackId { get; set; }
        public DateTime PaidAt { get; set; }
        public bool IsDeleted { get; set; }

        public IPaymentDomainService DomainService { get; set; }
        public IPaymentRepository Repository { get; set; }

        public Payment()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<IPaymentDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<IPaymentRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<Payment> GetAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<IPaymentRepository>();
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
