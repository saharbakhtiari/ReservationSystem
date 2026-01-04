using Domain.MemberProfiles;
using Domain.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Cartables
{
    public class Cartable : AuditableEntity
    {
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<MemberProfile> Users { get; set; }
        public ICartableDomainService DomainService { get; set; }
        public ICartableRepository Repository { get; set; }

        public Cartable()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<ICartableDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<ICartableRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
            Users = new HashSet<MemberProfile>();
        }
        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<Cartable> GetCartableByIdAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ICartableRepository>();
            var item = await repository.GetCartableByIdAsync(id, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
    }
}
