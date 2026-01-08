using Domain.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SpaceFiles
{
    public interface ISpaceFileRepository : IGenericRepository<SpaceFile, long>
    {
        Task BulkUpdateAsync(List<SpaceFile> files);
        Task<SpaceFile> GetAsync(Guid guid, CancellationToken cancellationToken);
        Task<SpaceFile> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
