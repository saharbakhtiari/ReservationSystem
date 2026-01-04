using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

namespace Domain.Externals.CMRServer
{
    public class CMRRequestDomainService : ICMRRequestDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public CMRRequestDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public CMRRequest OwnerEntity { get; set; }


    }
}
