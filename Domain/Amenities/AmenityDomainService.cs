using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

namespace Domain.Amenitys
{
    public class AmenityDomainService : IAmenityDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public AmenityDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public Amenity OwnerEntity { get; set; }


    }
}
