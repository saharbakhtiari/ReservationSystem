using Domain.Common.Interfaces;
using Domain.MemberProfiles;
using Domain.Spaces;
using Domain.UnitOfWork.Uow;
using Exceptions;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using System.Threading;

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
        public async Task SetSpace(long spaceId, CancellationToken cancellationToken)
        {
            if (spaceId > 0)
            {
                var space = await Space.GetAsync(spaceId, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
                OwnerEntity.Space = space;
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
