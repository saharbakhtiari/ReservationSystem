using Domain.Common.Interfaces;
using Domain.MemberProfiles;
using Domain.Spaces;
using Domain.TimeSlots;
using Domain.UnitOfWork.Uow;
using Exceptions;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Bookings
{
    public class BookingDomainService : IBookingDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;
        private readonly ICurrentUserService _currentUserService;


        public BookingDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer, ICurrentUserService currentUserService)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
            _currentUserService = currentUserService;
        }

        public Booking OwnerEntity { get; set; }
        public async Task SetTimeSlot(long slotId, CancellationToken cancellationToken)
        {
            if (slotId > 0)
            {
                var slot = await TimeSlot.GetAsync(slotId, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
                slot.IsBooked = true;
                await slot.SaveAsync(cancellationToken);
                OwnerEntity.TimeSlot = slot;
                OwnerEntity.TotalAmount = slot.Tariff.Price;
                OwnerEntity.Currency = slot.Tariff.Currency;
            }
        }

        public async Task SetProfile(CancellationToken cancellationToken)
        {
            var userid = _currentUserService.UserId.HasValue ? _currentUserService.UserId.Value : throw new UserFriendlyException(_localizer["User is not login"]);
            var profile = await MemberProfile.GetProfileAsync(userid, cancellationToken);
            OwnerEntity.Profile = profile;
        }
    }
}
