using Domain.BookingHolds;
using Domain.UnitOfWork.Uow;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Jobs
{
    public class ExpiredBookingManger
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ExpiredBookingManger(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task CancelExpiredBooking(CancellationToken cancellationToken = default)
        {
            using (var uow = _unitOfWorkManager.Begin(new SedUnitOfWorkOptions { IsTransactional = false }, requiresNew: true))
            {
                var expired = await new BookingHold().Repository.GetExpiredAsync(cancellationToken);
                if (expired is not null)
                {
                    expired.Status = Domain.Contract.Enums.BookingHoldStatus.Expired;
                    await expired.SaveAsync(cancellationToken);
                    await uow.CompleteAsync(cancellationToken);
                }
            }
        }
    }
}
