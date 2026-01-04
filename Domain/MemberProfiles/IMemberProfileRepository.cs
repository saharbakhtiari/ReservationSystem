using Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.MemberProfiles
{
    public interface IMemberProfileRepository : IGenericRepository<MemberProfile, long>
    {
        Task<MemberProfile> GetProfileAsync(Guid userId, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredProfileAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
        Task<MemberProfile> GetIncludedProfileAsync(Guid userId, CancellationToken cancellationToken);
    }
}
