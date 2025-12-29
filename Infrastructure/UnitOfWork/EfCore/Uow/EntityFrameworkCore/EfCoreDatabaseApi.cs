using System.Threading;
using System.Threading.Tasks;
using Domain.UnitOfWork.Uow;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork.EfCore.Uow.EntityFrameworkCore
{
    public class EfCoreDatabaseApi<TDbContext> : IDatabaseApi, ISupportsSavingChanges
        where TDbContext : IEfCoreDbContext
    {
        public TDbContext DbContext { get; }

        public EfCoreDatabaseApi(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}