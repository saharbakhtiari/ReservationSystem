using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Books
{
    public interface IBookRepository : IGenericRepository<Book, long>
    {
        Task<Book> GetAsync(long id, CancellationToken cancellationToken);
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
    }
}
