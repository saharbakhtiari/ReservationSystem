using Domain.Common.Interfaces;
using Domain.MemberProfiles;
using Domain.TimeSlots;
using Domain.UnitOfWork.Uow;
using Exceptions;
using Microsoft.Extensions.Localization;
using System;
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
        public async Task SetTimeSlot(long slotId, CancellationToken cancellationToken)
        {
            if (slotId > 0)
            {
                //using (var uow = _unitOfWorkManager.Begin(new SedUnitOfWorkOptions { IsTransactional = true, Timeout = TimeSpan.FromMinutes(5)}))
                //{

                //}
                var slot = await TimeSlot.GetAsync(slotId, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
                slot.IsHeld = true;
                await slot.SaveAsync(cancellationToken);
                OwnerEntity.TimeSlot = slot;
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
