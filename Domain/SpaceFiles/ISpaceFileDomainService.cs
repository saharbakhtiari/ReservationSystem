using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SpaceFiles
{
    public interface ISpaceFileDomainService : IBaseDomainService<SpaceFile>
    {
        Task<long> StoreFile(CancellationToken cancellationToken);
    }
}
