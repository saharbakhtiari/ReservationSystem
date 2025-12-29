using Domain.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Services
{
    public interface IBackgroundJobManager
    {
        Task RunAsync(IEnumerable<IJobService> jobServices, CancellationToken stopTokenSource);
    }

}
