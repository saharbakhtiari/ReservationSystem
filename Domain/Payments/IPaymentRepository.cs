using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Payments
{
    public interface IPaymentRepository : IGenericRepository<Payment, long>
    {
        Task<Payment> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
