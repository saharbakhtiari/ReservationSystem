using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.BookingDetails;
using Domain.Common;
using Extensions;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.BookingDetails
{
    public class BookingDetailRepository : GenericRepository<BookingDetail, long>, IBookingDetailRepository
    {
        public BookingDetailRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

       
    }
}
