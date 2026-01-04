using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common;
using Domain.Contract.Enums;
using Domain.Sliders;
using Extensions;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Sliders
{
    public class SliderRepository : GenericRepository<Slider, long>, ISliderRepository
    {
        public SliderRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<Slider> GetAsync(long id, CancellationToken cancellationToken)
        {
            return GetAllAsQueryable()
                .Include(a => a.Image)
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, cancellationToken);
        }
        public Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(SliderType? sliderType, string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken)
        {
            var mapper = ServiceLocator.ServiceProvider.GetService<IMapper>();
            return GetAllAsQueryable()
                .Where(x => !x.IsDeleted
                            && (sliderType == null || x.Type == sliderType))
                .WhereIf(filter.IsNullOrWhiteSpace().Not(), r => r.Title.Contains(filter))
                .OrderByIf(sort.IsNullOrWhiteSpace().Not(), sort)
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .ToPagedList(PageNumber, PageSize, cancellationToken);
        }
    }
}
