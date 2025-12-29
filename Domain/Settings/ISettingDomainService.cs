using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Settings
{
    public interface ISettingDomainService : IBaseDomainService<Setting>
    {
        Task Upsert(string key, string value, CancellationToken cancellationToken);
    }
}
