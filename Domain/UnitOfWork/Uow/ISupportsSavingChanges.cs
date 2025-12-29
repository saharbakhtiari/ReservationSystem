using System.Threading;
using System.Threading.Tasks;

namespace Domain.UnitOfWork.Uow
{
    public interface ISupportsSavingChanges
    {
        void SaveChanges();

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}