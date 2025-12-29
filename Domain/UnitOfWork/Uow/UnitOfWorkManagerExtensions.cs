using Domain.UnitOfWork;
using System;
using System.Data;

namespace Domain.UnitOfWork.Uow
{
    public static class UnitOfWorkManagerExtensions
    {
        public static IUnitOfWork Begin(
            this IUnitOfWorkManager unitOfWorkManager,
            bool requiresNew = false,
            bool isTransactional = false,
            IsolationLevel? isolationLevel = null,
            TimeSpan? timeout = null)
        {
            Check.NotNull(unitOfWorkManager, nameof(unitOfWorkManager));

            return unitOfWorkManager.Begin(new SedUnitOfWorkOptions
            {
                IsTransactional = isTransactional,
                IsolationLevel = isolationLevel,
                Timeout = timeout
            }, requiresNew);
        }

        public static void BeginReserved(this IUnitOfWorkManager unitOfWorkManager, string reservationName)
        {
            Check.NotNull(unitOfWorkManager, nameof(unitOfWorkManager));
            Check.NotNull(reservationName, nameof(reservationName));

            unitOfWorkManager.BeginReserved(reservationName, new SedUnitOfWorkOptions());
        }

        public static void TryBeginReserved(this IUnitOfWorkManager unitOfWorkManager, string reservationName)
        {
            Check.NotNull(unitOfWorkManager, nameof(unitOfWorkManager));
            Check.NotNull(reservationName, nameof(reservationName));

            unitOfWorkManager.TryBeginReserved(reservationName, new SedUnitOfWorkOptions());
        }
    }
}