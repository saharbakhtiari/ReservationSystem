using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Bookings;
using Domain.Common;
using Domain.Common.Interfaces;
using Extensions;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Bookings
{
    public class BookingRepository : GenericRepository<Booking, long>, IBookingRepository
    {
        private readonly ICurrentUserService _currentUserService;

        public BookingRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider, ICurrentUserService currentUserService) : base(dbContextProvider)
        {
            _currentUserService = currentUserService;
        }

        public Task<Booking> GetAsync(long id, bool isAdmin, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .WhereIf(!isAdmin, a => a.Profile.UserId == _currentUserService.UserId)
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, cancellationToken);
        }
        public Task<Booking> GetIncludedAsync(long id, bool isAdmin, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .Include(a => a.Profile)
                .Include(a => a.Details).ThenInclude(a => a.TimeSlot).ThenInclude(a => a.Space)
                .Include(a => a.Details).ThenInclude(a => a.TimeSlot).ThenInclude(a => a.Tariff)
                .Include(a => a.Participants)
                .WhereIf(!isAdmin, r => r.Profile.UserId == _currentUserService.UserId)
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, cancellationToken);
        }
        public Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, bool isAdmin, CancellationToken cancellationToken)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();
            return GetAllAsQueryable()
                .Where(x => !x.IsDeleted)
                .WhereIf(!isAdmin, r => r.Profile.UserId == _currentUserService.UserId)
                .WhereIf(filter.IsNullOrWhiteSpace().Not(), r => r.Profile.PhoneNumber.Contains(filter) || r.Details.Any(d => d.TimeSlot.Space.Title.Contains(filter)))
                .OrderByIf(sort.IsNullOrWhiteSpace().Not(), sort)
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .ToPagedList(PageNumber, PageSize, cancellationToken);
        }
    }
}
