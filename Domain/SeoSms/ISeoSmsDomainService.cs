using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SeoSms
{
    public interface ISeoSmsDomainService : IBaseDomainService<SeoSms>
    {
        Task DeleteOlds(CancellationToken cancellationToken);
    }
}
