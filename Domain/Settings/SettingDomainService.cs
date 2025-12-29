using Domain.UnitOfWork.Uow;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Settings
{
    public class SettingDomainService : ISettingDomainService
    {
        public Setting OwnerEntity { get; set; }
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SettingDomainService(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task Upsert(string key, string value, CancellationToken cancellationToken)
        {
            try
            {
                using (var uow = _unitOfWorkManager.Begin(new SedUnitOfWorkOptions { IsTransactional = false }, requiresNew: true))
                {
                    var setting = await OwnerEntity.Repository.GetSettingByKeyAsync(key, cancellationToken);
                    if (setting is null)
                    {
                        setting = new Setting()
                        {
                            Key = key,
                            Value = value,
                            LastModifiedUtc = DateTime.UtcNow
                        };
                    }
                    else
                    {
                        setting.Value = value;
                    }

                    await setting.SaveAsync(cancellationToken);
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
