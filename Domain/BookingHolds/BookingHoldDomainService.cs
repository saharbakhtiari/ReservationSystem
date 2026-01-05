using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

namespace Domain.BookingHolds
{
    public class BookingHoldDomainService : IBookingHoldDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public BookingHoldDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public BookingHold OwnerEntity { get; set; }


    }
}
