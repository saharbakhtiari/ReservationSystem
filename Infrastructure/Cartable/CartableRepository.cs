using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Cartables;
using Domain.Common;
using Extensions;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Cartables
{
    public class CartableRepository : GenericRepository<Cartable, long>, ICartableRepository
    {
        public CartableRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<Cartable> GetCartableByIdAsync(long id, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .Include(c => c.Users.Where(c => !c.IsDeleted))
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, cancellationToken);
        }

        public Task<PagedList<TOutput>> GetFilteredCartablesAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();
            return GetAllAsQueryable()
                .Where(x => !x.IsDeleted)
                .WhereIf(filter.IsNullOrWhiteSpace().Not(), r => r.Title.Contains(filter))
                .OrderByIf(sort.IsNullOrWhiteSpace().Not(), sort)
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .ToPagedList(PageNumber, PageSize, cancellationToken);
        }
        public Task<PagedList<TOutput>> GetMyCartablesAsync<TOutput>(Guid? userId, string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();
            return GetAllAsQueryable()
                .Where(x => !x.IsDeleted && x.Users.Any(u => u.UserId == userId))
                .WhereIf(filter.IsNullOrWhiteSpace().Not(), r => r.Title.Contains(filter))
                .OrderByIf(sort.IsNullOrWhiteSpace().Not(), sort)
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .ToPagedList(PageNumber, PageSize, cancellationToken);
        }

    }
}
