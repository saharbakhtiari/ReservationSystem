using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.BookingDetails
{
    public interface IBookingDetailRepository : IGenericRepository<BookingDetail, long>
    {
        
    }
}
