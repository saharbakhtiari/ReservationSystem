using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.BookingHoldDetails
{
    public interface IBookingHoldDetailRepository : IGenericRepository<BookingHoldDetail, long>
    {
        
    }
}
