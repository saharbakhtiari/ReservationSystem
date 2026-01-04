using Application.Sliders.Queries.GetFilteredSliders;
using Domain.Common;
using Domain.Sliders;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Sliders.Queries.GetFilteredSlider
{
    public class GetFilteredSlidersQueryHandler : IRequestHandler<GetFilteredSlidersQuery, PagedList<FilteredSlidersDto>>
    {
        public Task<PagedList<FilteredSlidersDto>> Handle(GetFilteredSlidersQuery request, CancellationToken cancellationToken)
        {
            return new Slider().Repository.GetFilteredAsync<FilteredSlidersDto>(request.Type, request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
