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

        //public async Task AddRuleToFavorits(long ruleVersionId, CancellationToken cancellationToken)
        //{
        //    var ruleVersion = await RuleVersion.GetRuleVersionAsync(ruleVersionId, cancellationToken)
        //        ?? throw new UserFriendlyException(_localizer["Rule version not found"]);
        //    if (OwnerEntity.FavoriteRules.Contains(ruleVersion).Not())
        //    {
        //        OwnerEntity.FavoriteRules.Add(ruleVersion);
        //    }
        //}

        //public async Task RemoveRuleFromFavorits(long ruleVersionId, CancellationToken cancellationToken)
        //{
        //    var ruleVersion = await RuleVersion.GetRuleVersionAsync(ruleVersionId, cancellationToken)
        //        ?? throw new UserFriendlyException(_localizer["Rule version not found"]);
        //    OwnerEntity.FavoriteRules.Remove(ruleVersion);
        //}
    }
}
