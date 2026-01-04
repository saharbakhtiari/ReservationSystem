using Domain.Amenitys;
using Domain.Common;
using Domain.Contract.Enums;
using Domain.SpaceFiles;
using System;
using System.Collections.Generic;
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
        public ICollection<SpaceFile> Images { get; set; }
        public string IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ISpaceDomainService DomainService { get; set; }
        public ISpaceRepository Repository { get; set; }

        public Space()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<ISpaceDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<ISpaceRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
            Amenities = new HashSet<Amenity>();
            Images = new HashSet<SpaceFile>();
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
    }
}
