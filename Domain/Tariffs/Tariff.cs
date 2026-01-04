using Domain.Amenitys;
using Domain.Common;
using Domain.Contract.Enums;
using Domain.SpaceFiles;
using Domain.Spaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Tariffs
{
    public class Tariff : AuditableEntity
    {
        public Space Space { get; set; } = null!;
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public TariffUnit Unit { get; set; }
        public string Rules { get; set; }
        public bool IsDeleted { get; set; }

        public ITariffDomainService DomainService { get; set; }
        public ITariffRepository Repository { get; set; }

        public Tariff()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<ITariffDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<ITariffRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<Tariff> GetAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ITariffRepository>();
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
