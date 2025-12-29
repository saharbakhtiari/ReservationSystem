using Domain.UnitOfWork;

namespace Domain.UnitOfWork.Uow
{
    public static class UnitOfWorkExtensions
    {
        public static bool IsReservedFor(this IUnitOfWork unitOfWork, string reservationName)
        {
            Check.NotNull(unitOfWork, nameof(unitOfWork));

            return unitOfWork.IsReserved && unitOfWork.ReservationName == reservationName;
        }
    }
}