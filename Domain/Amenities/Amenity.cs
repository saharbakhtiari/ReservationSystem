using Domain.Common;
using Domain.SpaceFiles;
using Domain.Spaces;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Amenitys
{
    public class Amenity : AuditableEntity
    {
        public string Title { get; set; } = null!;
        [JsonIgnore]
        public SpaceFile Icon { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public ICollection<Space> Spaces { get; set; }

        [JsonIgnore]
        public IAmenityDomainService DomainService { get; set; }
        [JsonIgnore]
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

        public static async Task<Amenity> GetIncludedAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<IAmenityRepository>();
            var item = await repository.GetIncludedAsync(id, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
    }
}
