using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common;
using Domain.SpaceFiles;
using Extensions;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.SpaceFiles
{
    public class SpaceFileRepository : GenericRepository<SpaceFile, long>, ISpaceFileRepository
    {
        public SpaceFileRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<SpaceFile> GetAsync(long id, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable().FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, cancellationToken);
        }

        public Task<SpaceFile> GetAsync(Guid guid, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable().FirstOrDefaultAsync(a => a.FileGuid == guid && !a.IsDeleted, cancellationToken);
        }

        public Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();
            return GetAllAsQueryable()
                .Where(x => !x.IsDeleted)
                .WhereIf(filter.IsNullOrWhiteSpace().Not(), r => r.FileType.Contains(filter))
                .OrderByIf(sort.IsNullOrWhiteSpace().Not(), sort)
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .ToPagedList(PageNumber, PageSize, cancellationToken);
        }
    }
}
