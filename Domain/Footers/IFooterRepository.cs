using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Footers
{
    public interface IFooterRepository : IGenericRepository<Footer, long>
    {
        Task<PagedList<TOutput>> GetFilteredFootersAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
        Task<Footer> GetFooterAsync(long id, CancellationToken cancellationToken);
    }
}
