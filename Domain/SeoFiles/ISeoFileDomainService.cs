using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SeoFiles
{
    public interface ISeoFileDomainService : IBaseDomainService<SeoFile>
    {
        Task<long> StoreFile(CancellationToken cancellationToken);
    }
}
