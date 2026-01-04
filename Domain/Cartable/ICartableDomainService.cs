using Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Cartables
{
    public interface ICartableDomainService : IBaseDomainService<Cartable>
    {
        Task AddUser(Guid userId, CancellationToken cancellationToken);
        Task RemoveUser(Guid userId, CancellationToken cancellationToken);
    }
}
