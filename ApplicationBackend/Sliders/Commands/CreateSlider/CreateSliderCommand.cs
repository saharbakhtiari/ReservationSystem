using Application.Sliders.Commands.CreateSlider;
using AutoMapper;
using Domain.Sliders;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Sliders.Commands.CreateSlider
{
    public class CreateSliderCommandHandler : IRequestHandler<CreateSliderCommand, long>
    {
        private readonly IMapper _mapper;

        public CreateSliderCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
        {
            var slider = _mapper.Map<Slider>(request);
            await slider.DomainService.SetImage(cancellationToken);
            await slider.SaveAsync(cancellationToken);
            return slider.Id;
        }
    }
}
