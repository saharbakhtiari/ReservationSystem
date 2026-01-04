using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Amenitys
{
    public interface IAmenityRepository : IGenericRepository<Amenity, long>
    {
        Task<Amenity> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
