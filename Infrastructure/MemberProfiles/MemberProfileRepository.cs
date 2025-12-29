using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common;
using Domain.MemberProfiles;
using Extensions;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.MemberProfiles
{
    public class MemberProfileRepository : GenericRepository<MemberProfile, long>, IMemberProfileRepository
    {
        public MemberProfileRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<MemberProfile> GetProfileAsync(Guid userId, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .FirstOrDefaultAsync(a => a.UserId == userId && !a.IsDeleted, cancellationToken);
        }

        public Task<MemberProfile> GetProfileAsync(string nationalId, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .FirstOrDefaultAsync(a => a.NationalId == nationalId && !a.IsDeleted, cancellationToken);
        }

        public Task<MemberProfile> GetIncludedProfileAsync(Guid userId, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .FirstOrDefaultAsync(a => a.UserId == userId && !a.IsDeleted, cancellationToken);
        }
        public Task<PagedList<TOutput>> GetFilteredProfileAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();
            return GetAllAsQueryable()
                .Where(x => !x.IsDeleted)
                .WhereIf(filter.IsNullOrWhiteSpace().Not(), r => r.FirstName.Contains(filter) || r.LastName.Contains(filter))
                .OrderByIf(sort.IsNullOrWhiteSpace().Not(), sort)
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .ToPagedList(PageNumber, PageSize, cancellationToken);
        }
    }
}
