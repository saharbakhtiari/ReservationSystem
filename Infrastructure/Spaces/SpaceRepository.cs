using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common;
using Domain.Contract.Enums;
using Domain.Spaces;
using Extensions;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Spaces
{
    public class SpaceRepository : GenericRepository<Space, long>, ISpaceRepository
    {
        public SpaceRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<Space> GetAsync(long id, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, cancellationToken);
        }
        public Task<PagedList<TOutput>> GetFilteredAsync<TOutput>( string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();
            return GetAllAsQueryable()
                .Where(x => !x.IsDeleted)
                .WhereIf(filter.IsNullOrWhiteSpace().Not(), r => r.Title.Contains(filter))
                .OrderByIf(sort.IsNullOrWhiteSpace().Not(), sort)
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .ToPagedList(PageNumber, PageSize, cancellationToken);
        }

    }
}
