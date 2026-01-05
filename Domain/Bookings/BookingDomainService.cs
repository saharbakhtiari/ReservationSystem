using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

namespace Domain.Bookings
{
    public class BookingDomainService : IBookingDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public BookingDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public Booking OwnerEntity { get; set; }


    }
}
