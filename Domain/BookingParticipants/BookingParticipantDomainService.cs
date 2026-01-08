using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;

namespace Domain.BookingParticipants
{
    public class BookingParticipantDomainService : IBookingParticipantDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public BookingParticipantDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public BookingParticipant OwnerEntity { get; set; }


    }
}
