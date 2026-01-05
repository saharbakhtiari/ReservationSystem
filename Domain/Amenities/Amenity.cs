using Domain.Common;
using Domain.SpaceFiles;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Amenitys
{
    public class Amenity : AuditableEntity
    {
        public string Title { get; set; } = null!;
        public SpaceFile Icon { get; set; }
        public bool IsDeleted { get; set; }

        public IAmenityDomainService DomainService { get; set; }
        public IAmenityRepository Repository { get; set; }

        public Amenity()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<IAmenityDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<IAmenityRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<Amenity> GetAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<IAmenityRepository>();
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
