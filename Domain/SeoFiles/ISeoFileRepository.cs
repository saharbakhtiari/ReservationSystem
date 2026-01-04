using Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SeoFiles
{
    public interface ISeoFileRepository : IGenericRepository<SeoFile, long>
    {
        Task<SeoFile> GetAsync(Guid guid, CancellationToken cancellationToken);
        Task<SeoFile> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
