using Domain.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SeoSms
{
    public interface ISeoSmsRepository : IGenericRepository<SeoSms, long>
    {
        Task<List<SeoSms>> GetOldSmsesAsync(CancellationToken cancellationToken);
        Task<SeoSms> GetSmsAsync(string phoneNumber, CancellationToken cancellationToken);
    }
}
