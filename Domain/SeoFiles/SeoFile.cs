using Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SeoFiles
{
    public class SeoFile : FileEntity
    {
        public bool IsDeleted { get; set; }

        public ISeoFileDomainService DomainService { get; set; }
        public ISeoFileRepository Repository { get; set; }

        public SeoFile()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<ISeoFileDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<ISeoFileRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public static async Task<SeoFile> GetFileAsync(Guid guid, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ISeoFileRepository>();
            var item = await repository.GetAsync(guid, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
        public static async Task<SeoFile> GetFileAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ISeoFileRepository>();
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
