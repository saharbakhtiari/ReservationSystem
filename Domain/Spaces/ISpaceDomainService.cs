using Domain.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Spaces
{
    public interface ISpaceDomainService : IBaseDomainService<Space>
    {
        Task CreateImages(CancellationToken cancellationToken);
        Task SetAmenities(List<long> Ids, CancellationToken cancellationToken);
        Task UpdateImages(CancellationToken cancellationToken);
    }
}
