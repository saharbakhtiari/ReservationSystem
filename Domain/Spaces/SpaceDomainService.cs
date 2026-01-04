using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

namespace Domain.Spaces
{
    public class SpaceDomainService : ISpaceDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public SpaceDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public Space OwnerEntity { get; set; }


    }
}
