using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

namespace Domain.Payments
{
    public class PaymentDomainService : IPaymentDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public PaymentDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public Payment OwnerEntity { get; set; }


    }
}
