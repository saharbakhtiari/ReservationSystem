using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Invoices
{
    public interface IInvoiceRepository : IGenericRepository<Invoice, long>
    {
        Task<Invoice> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
