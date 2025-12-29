using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Settings
{
    public interface ISettingRepository : IGenericRepository<Setting, long>
    {
        Task<Setting> GetSettingByKeyAsync(string key, CancellationToken cancellationToken);
    }
}
