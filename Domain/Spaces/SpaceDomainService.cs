using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;
using System.Linq;
using Domain.SpaceFiles;
using Domain.Amenitys;

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

        public async Task SetAmenities(List<long> Ids,CancellationToken cancellationToken)
        {
            List<Amenity> amenities = new();
            foreach(var id in Ids)
            {
                var amenity = await Amenity.GetAsync(id, cancellationToken);
                amenities.Add(amenity);
            }
            OwnerEntity.Amenities = amenities;
        }

        public async Task CreateImages(CancellationToken cancellationToken)
        {
            foreach (var item in OwnerEntity.Images)
            {
                await item.DomainService.StoreFile(cancellationToken);
            }
        }

        public async Task UpdateImages(CancellationToken cancellationToken)
        {
            var ExistImages = OwnerEntity.Images.Where(a => a.Id > 0).ToList();
            var newImages = OwnerEntity.Images.Where(a => a.Id < 1).ToList();
            var deleteImages = OwnerEntity.Images
                                        .Where(e => e.Id > 0) // ← آیتم‌های صفر حذف
                                        .Where(e => !ExistImages
                                        .Where(x => x.Id > 0)
                                        .Any(x => x.Id == e.Id))
                                        .ToList();
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

        private async Task RemoveImages(List<SpaceFile> websites)
        {
            using (var uow1 = _unitOfWorkManager.Begin(new SedUnitOfWorkOptions { IsTransactional = true, Timeout = TimeSpan.FromMinutes(10) }, requiresNew: true))
            {
                foreach (var item in websites)
                {
                    item.IsDeleted = true;
                }
                await new SpaceFile().Repository.BulkUpdateAsync(websites);
            }
        }
    }
}
