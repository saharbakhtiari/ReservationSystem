using System;
using System.Data;

namespace Domain.UnitOfWork.Uow
{
    public interface ISedUnitOfWorkOptions
    {
        bool IsTransactional { get; }

        IsolationLevel? IsolationLevel { get; }

        TimeSpan? Timeout { get; }
    }
}