using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Tariffs
{
    public interface ITariffDomainService : IBaseDomainService<Tariff>
    {
        Task SetSpace(long spaceId, CancellationToken cancellationToken);
    }
}
