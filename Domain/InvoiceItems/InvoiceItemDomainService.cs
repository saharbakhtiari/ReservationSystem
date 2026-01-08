using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

namespace Domain.InvoiceItems
{
    public class InvoiceItemDomainService : IInvoiceItemDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public InvoiceItemDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public InvoiceItem OwnerEntity { get; set; }


    }
}
