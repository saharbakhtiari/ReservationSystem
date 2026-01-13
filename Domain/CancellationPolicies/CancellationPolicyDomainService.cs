using Domain.Spaces;
using Domain.UnitOfWork.Uow;
using Exceptions;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using System.Threading;
using Domain.Tariffs;

namespace Domain.CancellationPolicys
{
    public class CancellationPolicyDomainService : ICancellationPolicyDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public CancellationPolicyDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public CancellationPolicy OwnerEntity { get; set; }
        public async Task SetTariff(long tariffId, CancellationToken cancellationToken)
        {
            if (tariffId > 0)
            {
                var space = await Tariff.GetAsync(tariffId, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]); ;
                OwnerEntity.Tariff = space;
            }
        }

    }
}
