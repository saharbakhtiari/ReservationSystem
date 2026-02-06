using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.BookingHolds;
using Domain.Common;
using Extensions;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.BookingHolds
{
    public class BookingHoldRepository : GenericRepository<BookingHold, long>, IBookingHoldRepository
    {
        public BookingHoldRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<BookingHold> GetAsync(long id, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, cancellationToken);
        }
        public Task<BookingHold> GetIncludedAsync(long id, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .Include(a => a.Details).ThenInclude(a => a.TimeSlot).ThenInclude(a=>a.Space)
                .Include(a => a.Details).ThenInclude(a => a.TimeSlot).ThenInclude(a=>a.Tariff)
                .Include(a => a.Profile)
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, cancellationToken);
        }
        public Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();
            return GetAllAsQueryable()
                .Where(x => !x.IsDeleted)
                .WhereIf(filter.IsNullOrWhiteSpace().Not(), r => r.Profile.PhoneNumber.Contains(filter) || r.Details.Any(d => d.TimeSlot.Space.Title.Contains(filter)))
                .OrderByIf(sort.IsNullOrWhiteSpace().Not(), sort)
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .ToPagedList(PageNumber, PageSize, cancellationToken);
        }
    }
}
