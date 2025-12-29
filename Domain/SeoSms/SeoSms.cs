using Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SeoSms
{
    public class SeoSms : Entity
    {
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public string VerifyCode { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ExpireDate { get; set; }

        public ISeoSmsDomainService DomainService { get; set; }
        public ISeoSmsRepository Repository { get; set; }

        public SeoSms()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<ISeoSmsDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<ISeoSmsRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }
        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<SeoSms> GetSmsAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ISeoSmsRepository>();
            var item = await repository.GetSmsAsync(phoneNumber, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
    }
}
