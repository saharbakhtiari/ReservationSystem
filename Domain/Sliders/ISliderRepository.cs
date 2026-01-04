using Domain.Common;
using Domain.Contract.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Sliders
{
    public interface ISliderRepository : IGenericRepository<Slider, long>
    {
        Task<PagedList<TOutput>> GetFilteredAsync<TOutput>(SliderType? sliderType, string filter, string sort, int PageNumber, int PageSize, CancellationToken cancellationToken);
        Task<Slider> GetAsync(long id, CancellationToken cancellationToken);
    }
}
