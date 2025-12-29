using System.Threading;
using System.Threading.Tasks;

namespace Domain.UnitOfWork.Uow
{
    public interface ISupportsRollback
    {
        void Rollback();

        Task RollbackAsync(CancellationToken cancellationToken);
    }
}