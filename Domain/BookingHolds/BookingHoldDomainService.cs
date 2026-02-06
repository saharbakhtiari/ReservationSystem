using Domain.BookingHoldDetails;
using Domain.Common.Interfaces;
using Domain.MemberProfiles;
using Domain.TimeSlots;
using Domain.UnitOfWork.Uow;
using Exceptions;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.BookingHolds
{
    public class BookingHoldDomainService : IBookingHoldDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;
        private readonly ICurrentUserService _currentUserService;


        public BookingHoldDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer, ICurrentUserService currentUserService)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
            _currentUserService = currentUserService;
        }

        public BookingHold OwnerEntity { get; set; }
        public async Task SetTimeSlot(long slotId, int count, CancellationToken cancellationToken)
        {
            if (slotId > 0)
            {
                var slot = await TimeSlot.GetAsync(slotId, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
                if(slot.AvailableCount < count)
                {
                    throw new UserFriendlyException(_localizer["Item is not available"]);
                }
                slot.AvailableCount -= count;
                await slot.SaveAsync(cancellationToken);
                OwnerEntity.Details.Add(new BookingHoldDetail()
                {
                    Count = count,
                    TimeSlot = slot
                });
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
