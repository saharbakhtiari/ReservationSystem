using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.BookingHolds;
using Domain.Common;
using Domain.Common.Interfaces;
using Domain.Contract.Enums;
using Extensions;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.BookingHolds
{
    public class BookingHoldRepository : GenericRepository<BookingHold, long>, IBookingHoldRepository
    {
        private readonly ICurrentUserService _currentUserService;

        public BookingHoldRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider, ICurrentUserService currentUserService) : base(dbContextProvider)
        {
            _currentUserService = currentUserService;
        }

        public Task<BookingHold> GetAsync(long id, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, cancellationToken);
        }

        public Task<BookingHold> GetExpiredAsync(CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .OrderBy(a => a.Id)
                .FirstOrDefaultAsync(a => !a.IsDeleted
                                          && a.ExpireAt < DateTime.Now 
                                          && a.Status != BookingHoldStatus.Completed 
                                          && a.Status != BookingHoldStatus.Expired
                                          , cancellationToken);
        }

        public Task<BookingHold> GetIncludedAsync(long id, bool isAdmin, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .Include(a => a.Details).ThenInclude(a => a.TimeSlot).ThenInclude(a => a.Space)
                .Include(a => a.Details).ThenInclude(a => a.TimeSlot).ThenInclude(a => a.Tariff)
                .Include(a => a.Profile)
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
