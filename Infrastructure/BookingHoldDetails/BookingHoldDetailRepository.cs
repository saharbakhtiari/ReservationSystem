using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.BookingHoldDetails;
using Domain.Common;
using Extensions;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.BookingHoldDetails
{
    public class BookingHoldDetailRepository : GenericRepository<BookingHoldDetail, long>, IBookingHoldDetailRepository
    {
        public BookingHoldDetailRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

       
    }
}
