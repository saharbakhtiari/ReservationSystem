using Domain.Spaces;
using Domain.UnitOfWork.Uow;
using Exceptions;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Tariffs
{
    public class TariffDomainService : ITariffDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public TariffDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public Tariff OwnerEntity { get; set; }

        public async Task SetSpace(long spaceId, CancellationToken cancellationToken)
        {
            if (spaceId > 0)
            {
                var space = await Space.GetAsync(spaceId, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]); ;
                OwnerEntity.Space = space;
            }
        }
    }
}
