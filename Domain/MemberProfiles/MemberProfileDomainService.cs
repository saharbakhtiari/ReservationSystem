using Microsoft.Extensions.Localization;

namespace Domain.MemberProfiles
{
    public class MemberProfileDomainService : IMemberProfileDomainService
    {
        public MemberProfile OwnerEntity { get; set; }
        private readonly IStringLocalizer _localizer;

        public MemberProfileDomainService(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }
    }
}
