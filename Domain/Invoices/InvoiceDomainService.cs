using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

namespace Domain.Invoices
{
    public class InvoiceDomainService : IInvoiceDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public InvoiceDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public Invoice OwnerEntity { get; set; }


    }
}
