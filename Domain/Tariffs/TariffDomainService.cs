using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

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


    }
}
