using System.Threading;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Services
{
    public interface IJobServiceAdmin
    {
        Task RunAsync(CancellationTokenSource cancellationTokenSource);
    }

}
