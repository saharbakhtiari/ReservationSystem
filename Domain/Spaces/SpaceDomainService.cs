using Domain.Amenitys;
using Domain.SpaceFiles;
using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Spaces
{
    public class SpaceDomainService : ISpaceDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public SpaceDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public Space OwnerEntity { get; set; }

        public async Task SetAmenities(List<long> Ids, CancellationToken cancellationToken)
        {
            List<Amenity> amenities = new();
            foreach (var id in Ids)
            {
                var amenity = await Amenity.GetAsync(id, cancellationToken);
                amenities.Add(amenity);
            }
            OwnerEntity.Amenities = amenities;
        }

        public async Task UpdateAmenities(List<long> Ids, CancellationToken cancellationToken)
        {
            List<Amenity> amenities = new();
            var deleteAminity = OwnerEntity.Amenities.Where(a => !Ids.Contains(a.Id)).Select(a => a.Id).ToList();
            var AddAminity = OwnerEntity.Amenities.Where(a => Ids.Contains(a.Id)).Select(a => a.Id).ToList();
            foreach (var id in AddAminity)
            {
                var amenity = await Amenity.GetAsync(id, cancellationToken);
                amenities.Add(amenity);
            }
            foreach (var id in deleteAminity)
            {
                var amenity = await Amenity.GetAsync(id, cancellationToken);
                amenities.Remove(amenity);
            }
            OwnerEntity.Amenities = amenities;
        }

        public async Task CreateImages(CancellationToken cancellationToken)
        {
            foreach (var item in OwnerEntity.Gallery)
            {
                await item.DomainService.StoreFile(cancellationToken);
            }
        }

        public async Task UpdateImages(List<long> UnChangedGalleryImageIds, CancellationToken cancellationToken)
        {
            var newImages = OwnerEntity.Gallery.Where(a => a.Id < 1).ToList();
            var entity = await Space.GetIncludedAsync(OwnerEntity.Id, cancellationToken);
            var deleteImages = entity.Gallery.Where(a => !UnChangedGalleryImageIds.Contains(a.Id) && a.Id > 0).ToList();
            await AddNewImages(newImages, cancellationToken);
            await RemoveImages(deleteImages);
        }

        private async Task AddNewImages(List<SpaceFile> newImages, CancellationToken cancellationToken)
        {
            foreach (var item in newImages)
            {
                {
                    await item.DomainService.StoreFile(cancellationToken);
                }
            }
        }

        private async Task RemoveImages(List<SpaceFile> deleteImages)
        {
            using (var uow1 = _unitOfWorkManager.Begin(new SedUnitOfWorkOptions { IsTransactional = true, Timeout = TimeSpan.FromMinutes(10) }, requiresNew: true))
            {
                foreach (var item in deleteImages)
                {
                    item.IsDeleted = true;
                }
                await new SpaceFile().Repository.BulkUpdateAsync(deleteImages);
            }
        }
    }
}
