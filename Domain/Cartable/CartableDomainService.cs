using Domain.MemberProfiles;
using Exceptions;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Cartables
{
    public class CartableDomainService : ICartableDomainService
    {
        public Cartable OwnerEntity { get; set; }

        private readonly IStringLocalizer _localizer;

        public CartableDomainService(IStringLocalizer localizer)
        {

            _localizer = localizer;
        }

        public async Task AddUser(Guid userId, CancellationToken cancellationToken)
        {
            var user = await MemberProfile.GetProfileAsync(userId, cancellationToken) ?? throw new UserFriendlyException(_localizer["User profile is not defined"]);
            OwnerEntity.Users.Add(user);
        }

        public async Task RemoveUser(Guid userId, CancellationToken cancellationToken)
        {
            var user = await MemberProfile.GetProfileAsync(userId, cancellationToken) ?? throw new UserFriendlyException(_localizer["User profile is not defined"]);
            OwnerEntity.Users.Remove(user);
        }

    }
}
