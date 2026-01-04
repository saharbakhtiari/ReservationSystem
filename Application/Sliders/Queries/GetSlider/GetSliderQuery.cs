using MediatR;

namespace Application.Sliders.Queries.GetSlider
{
    public class GetSliderQuery : IRequest<SliderDto>
    {
        public long Id { get; set; }
    }
}
