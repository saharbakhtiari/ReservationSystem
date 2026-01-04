using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Cartables
{
    public interface ICartableRepository : IGenericRepository<Cartable, long>
    {
        Task<PagedList<TOutput>> GetFilteredCartablesAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);

        Task<PagedList<TOutput>> GetMyCartablesAsync<TOutput>(System.Guid? userId,string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
        Task<Cartable> GetCartableByIdAsync(long id, CancellationToken cancellationToken);
    }
}
