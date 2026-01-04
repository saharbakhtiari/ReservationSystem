using Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SliderFiles
{
    public class SliderFile : FileEntity
    {
        public bool IsDeleted { get; set; }

        public ISliderFileDomainService DomainService { get; set; }
        public ISliderFileRepository Repository { get; set; }

        public SliderFile()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<ISliderFileDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<ISliderFileRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public static async Task<SliderFile> GetFileAsync(Guid guid, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ISliderFileRepository>();
            var item = await repository.GetAsync(guid, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
        public static async Task<SliderFile> GetFileAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ISliderFileRepository>();
            var item = await repository.GetAsync(id, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }
    }
}
