using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Headers
{
    public interface IHeaderRepository : IGenericRepository<Header, long>
    {
        Task<PagedList<TOutput>> GetFilteredHeadersAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
        Task<Header> GetHeaderAsync(long id, CancellationToken cancellationToken);
    }
}
