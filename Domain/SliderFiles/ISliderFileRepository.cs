using Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SliderFiles
{
    public interface ISliderFileRepository : IGenericRepository<SliderFile, long>
    {
        Task<SliderFile> GetAsync(Guid guid, CancellationToken cancellationToken);
        Task<SliderFile> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
