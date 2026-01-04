using Domain.SeoSms;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.SeoSmss
{
    public class SeoSmsRepository : GenericRepository<SeoSms, long>, ISeoSmsRepository
    {
        public SeoSmsRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<SeoSms> GetSmsAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .AsNoTracking()
                .OrderByDescending(x => x.IssuedDate)
                .FirstOrDefaultAsync(a => a.PhoneNumber == phoneNumber && !a.IsDeleted, cancellationToken);
        }

        public Task<List<SeoSms>> GetOldSmsesAsync(CancellationToken cancellationToken)
        {
            return GetAllAsQueryable().Where(a => !a.IsDeleted && a.PhoneNumber == OwnerEntity.PhoneNumber).ToListAsync(cancellationToken);
        }
    }
}
