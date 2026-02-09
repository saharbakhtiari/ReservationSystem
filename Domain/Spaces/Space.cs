using Domain.Amenitys;
using Domain.Common;
using Domain.Contract.Enums;
using Domain.SpaceFiles;
using Domain.Tariffs;
using Domain.TimeSlots;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Spaces
{
    public class Space : AuditableEntity
    {
        public string Title { get; set; } = null!;
        public int Capacity { get; set; }
        public string Location { get; set; }
        public SpaceType Type { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        [JsonIgnore]
        public ICollection<SpaceFile> Gallery { get; set; }
        [JsonIgnore]
        public ICollection<Tariff> Tariffs { get; set; }
        [JsonIgnore]
        public ICollection<TimeSlot> TimeSlots { get; set; }
        [JsonIgnore]
        public SpaceFile MainImage { get; set; }
        public string IsActive { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public ISpaceDomainService DomainService { get; set; }
        [JsonIgnore]
        public ISpaceRepository Repository { get; set; }

        public Space()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<ISpaceDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<ISpaceRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
            Amenities = new HashSet<Amenity>();
            Gallery = new HashSet<SpaceFile>();
            TimeSlots = new HashSet<TimeSlot>();
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<Space> GetAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ISpaceRepository>();
            var item = await repository.GetAsync(id, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }

        public static async Task<Space> GetIncludedAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ISpaceRepository>();
            var item = await repository.GetIncludedAsync(id, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
        public static async Task<Space> GetIncludedAmenityAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ISpaceRepository>();
            var item = await repository.GetIncludedAmenityAsync(id, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
    }
}
