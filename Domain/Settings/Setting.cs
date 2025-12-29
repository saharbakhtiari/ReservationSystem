using Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Settings
{
    public class Setting : AuditableEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public byte[] RowVersion { get; set; }

        public ISettingDomainService DomainService { get; set; }
        public ISettingRepository Repository { get; set; }

        public Setting()
        {
            Repository = ServiceLocator.ServiceProvider.GetService<ISettingRepository>();
            DomainService=ServiceLocator.ServiceProvider.GetService<ISettingDomainService>();
            Repository.OwnerEntity = this;
            DomainService.OwnerEntity = this;
        }

        public static async Task<Setting> GetSettingByKeyAsync(string key,CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ISettingRepository>();
            var item = await repository.GetSettingByKeyAsync(key, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }

 
        public override Task SaveAsync(CancellationToken cancellationToken)
        {
            return Repository.SaveAsync(cancellationToken);
        }
    }
}
