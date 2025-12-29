using Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.MemberProfiles
{
    public class MemberProfile : AuditableEntity
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }


        public IMemberProfileDomainService DomainService { get; set; }
        public IMemberProfileRepository Repository { get; set; }

        public MemberProfile()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<IMemberProfileDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<IMemberProfileRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;

        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<MemberProfile> GetProfileAsync(Guid userId, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<IMemberProfileRepository>();
            var item = await repository.GetProfileAsync(userId, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
        public static async Task<MemberProfile> GetProfileAsync(string nationalId, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<IMemberProfileRepository>();
            var item = await repository.GetProfileAsync(nationalId, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }

        public static async Task<MemberProfile> GetIncludedProfileAsync(Guid userId, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<IMemberProfileRepository>();
            var item = await repository.GetIncludedProfileAsync(userId, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
    }
}
