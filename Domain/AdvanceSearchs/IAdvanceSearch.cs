using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.AdvanceSearchs
{
    public interface IAdvanceSearch<TOutput> where TOutput : class
    {
        Task<PagedList<TOutput>> Search(AdvanceSearchInputDto dto, CancellationToken cancellationToken);

    }

}
