using Domain.Amenitys;
using Domain.Common;
using Domain.Contract.Enums;
using Domain.SpaceFiles;
using Domain.Spaces;
using Domain.Tariffs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CancellationPolicys
{
    public class CancellationPolicy : AuditableEntity
    {
        public Space Space { get; set; } = null!;
        public Tariff Tariff { get; set; } = null!;
        public int FreeCancelUntilHours { get; set; }
        public int PenaltyPercentAfter { get; set; }
        public int NoShowPenalty { get; set; }
        public bool IsDeleted { get; set; }

        public ICancellationPolicyDomainService DomainService { get; set; }
        public ICancellationPolicyRepository Repository { get; set; }

        public CancellationPolicy()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<ICancellationPolicyDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<ICancellationPolicyRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<CancellationPolicy> GetAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ICancellationPolicyRepository>();
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
