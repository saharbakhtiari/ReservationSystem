using Domain.Amenitys;
using Domain.Bookings;
using Domain.Common;
using Domain.Contract.Enums;
using Domain.Invoices;
using Domain.MemberProfiles;
using Domain.SpaceFiles;
using Domain.Spaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.InvoiceItems
{
    public class InvoiceItem : AuditableEntity
    {
        public Invoice Invoice { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public bool IsDeleted { get; set; }

        public IInvoiceItemDomainService DomainService { get; set; }
        public IInvoiceItemRepository Repository { get; set; }

        public InvoiceItem()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<IInvoiceItemDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<IInvoiceItemRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<InvoiceItem> GetAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<IInvoiceItemRepository>();
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
