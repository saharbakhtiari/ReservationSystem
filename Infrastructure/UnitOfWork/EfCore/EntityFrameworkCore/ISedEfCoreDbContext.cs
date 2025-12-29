namespace Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore
{
    public interface ISedEfCoreDbContext : IEfCoreDbContext
    {
        void Initialize(SedEfCoreDbContextInitializationContext initializationContext);
    }
}