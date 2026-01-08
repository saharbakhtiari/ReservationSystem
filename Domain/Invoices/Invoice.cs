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

namespace Domain.Invoices
{
    public class Invoice : AuditableEntity
    {
        public Booking Booking { get; set; }
        public string InvoiceNumber { get; set; }
        public InvoiceStatus Status { get; set; }
        public string CustomerInfoSnapshot { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public DateTime IssuedAt { get; set; }
        public bool IsDeleted { get; set; }

        public IInvoiceDomainService DomainService { get; set; }
        public IInvoiceRepository Repository { get; set; }

        public Invoice()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<IInvoiceDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<IInvoiceRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<Invoice> GetAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<IInvoiceRepository>();
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
