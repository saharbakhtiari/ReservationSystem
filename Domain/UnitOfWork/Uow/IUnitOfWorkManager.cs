using System;

namespace Domain.UnitOfWork.Uow
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork Current { get; }

        IUnitOfWork Begin(SedUnitOfWorkOptions options, bool requiresNew = false);

        IUnitOfWork Reserve(string reservationName, bool requiresNew = false);

        void BeginReserved(string reservationName, SedUnitOfWorkOptions options);

        bool TryBeginReserved(string reservationName, SedUnitOfWorkOptions options);
    }
}