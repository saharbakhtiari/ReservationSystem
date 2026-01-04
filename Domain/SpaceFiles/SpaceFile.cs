using Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SpaceFiles
{
    public class SpaceFile : FileEntity
    {
        public int Order { get; set; }
        public bool IsDeleted { get; set; }

        public ISpaceFileDomainService DomainService { get; set; }
        public ISpaceFileRepository Repository { get; set; }

        public SpaceFile()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<ISpaceFileDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<ISpaceFileRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public static async Task<SpaceFile> GetFileAsync(Guid guid, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ISpaceFileRepository>();
            var item = await repository.GetAsync(guid, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
        public static async Task<SpaceFile> GetFileAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ISpaceFileRepository>();
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
