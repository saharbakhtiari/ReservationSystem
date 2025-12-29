namespace Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore.DependencyInjection
{
    public interface ISedDbContextConfigurer
    {
        void Configure(SedDbContextConfigurationContext context);
    }

    public interface ISedDbContextConfigurer<TDbContext>
        where TDbContext : SedDbContext<TDbContext>
    {
        void Configure(SedDbContextConfigurationContext<TDbContext> context);
    }
}