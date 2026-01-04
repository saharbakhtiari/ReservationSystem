using Application.Sliders.Queries.GetSlider;
using AutoMapper;
using Domain.Sliders;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Sliders.Queries.GetSlider
{
    public class GetSliderQueryHandler : IRequestHandler<GetSliderQuery, SliderDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetSliderQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<SliderDto> Handle(GetSliderQuery request, CancellationToken cancellationToken)
        {
            var slider = await Slider.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Slider not found"]);
            return _mapper.Map<SliderDto>(slider);
        }
    }
}
