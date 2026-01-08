using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.InvoiceItems
{
    public interface IInvoiceItemRepository : IGenericRepository<InvoiceItem, long>
    {
        Task<InvoiceItem> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
