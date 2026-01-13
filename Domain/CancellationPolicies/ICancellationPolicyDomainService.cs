using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CancellationPolicys
{
    public interface ICancellationPolicyDomainService : IBaseDomainService<CancellationPolicy>
    {
        Task SetTariff(long tariffId, CancellationToken cancellationToken);
    }
}
