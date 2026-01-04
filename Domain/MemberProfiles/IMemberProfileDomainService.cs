using Domain.Common;

namespace Domain.MemberProfiles
{
    public interface IMemberProfileDomainService : IBaseDomainService<MemberProfile>
    {
        //Task AddRuleToFavorits(long ruleVersionId, CancellationToken cancellationToken);
        //Task RemoveRuleFromFavorits(long ruleVersionId, CancellationToken cancellationToken);
    }
}
