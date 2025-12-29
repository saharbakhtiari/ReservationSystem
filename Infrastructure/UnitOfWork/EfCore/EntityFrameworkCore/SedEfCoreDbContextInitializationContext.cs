using Domain.UnitOfWork.Uow;

namespace Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore
{
    public class SedEfCoreDbContextInitializationContext
    {
        public IUnitOfWork UnitOfWork { get; }

        public SedEfCoreDbContextInitializationContext(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}