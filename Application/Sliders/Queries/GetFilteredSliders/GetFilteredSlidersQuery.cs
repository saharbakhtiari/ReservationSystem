using Domain.Common;
using Domain.Contract.Enums;
using MediatR;

namespace Application.Sliders.Queries.GetFilteredSliders
{
    public class GetFilteredSlidersQuery : IRequest<PagedList<FilteredSlidersDto>>
    {
        public SliderType Type { get; set; }
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
