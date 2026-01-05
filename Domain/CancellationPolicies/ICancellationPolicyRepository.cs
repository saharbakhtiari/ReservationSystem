using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CancellationPolicys
{
    public interface ICancellationPolicyRepository : IGenericRepository<CancellationPolicy, long>
    {
        Task<CancellationPolicy> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
