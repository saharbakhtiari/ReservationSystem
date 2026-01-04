using Domain.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Common
{
    /// <summary>
    /// Entity base class
    /// </summary>
    public abstract class ApiCallerEntity : IEntity
    {
        public abstract Task SendGetAsync<TResponse>(string url, CancellationToken cancellationToken);
    }
}
