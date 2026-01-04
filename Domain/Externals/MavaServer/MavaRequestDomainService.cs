using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

namespace Domain.Externals.MavaServer
{
    public class MavaRequestDomainService : IMavaRequestDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public MavaRequestDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public MavaRequest OwnerEntity { get; set; }


    }
}
