using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

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


    }
}
