using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SliderFiles
{
    public interface ISliderFileDomainService : IBaseDomainService<SliderFile>
    {
        Task<long> StoreFile(CancellationToken cancellationToken);
    }
}
