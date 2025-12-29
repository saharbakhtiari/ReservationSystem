using System;
using System.Threading.Tasks;

namespace Domain.UnitOfWork.Uow
{
    public interface ITransactionApi : IDisposable
    {
        void Commit();

        Task CommitAsync();
    }
}