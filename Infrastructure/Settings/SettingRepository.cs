using Domain.Settings;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Settings
{
    public class SettingRepository : GenericRepository<Setting, long>, ISettingRepository
    {
        public SettingRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<Setting> GetSettingByKeyAsync(string key, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable().FirstOrDefaultAsync(a => a.Key == key, cancellationToken);
        }
    }
}
