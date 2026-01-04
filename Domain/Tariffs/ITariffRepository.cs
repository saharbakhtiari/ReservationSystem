using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Tariffs
{
    public interface ITariffRepository : IGenericRepository<Tariff, long>
    {
        Task<Tariff> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
