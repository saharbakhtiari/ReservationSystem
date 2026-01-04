using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Spaces
{
    public interface ISpaceRepository : IGenericRepository<Space, long>
    {
        Task<Space> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
