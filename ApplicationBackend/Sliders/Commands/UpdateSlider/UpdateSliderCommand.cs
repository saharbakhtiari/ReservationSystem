using Application.Sliders.Commands.UpdateSlider;
using AutoMapper;
using Domain.Sliders;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Sliders.Commands.UpdateSlider
{
    public class UpdateSliderCommandHandler : IRequestHandler<UpdateSliderCommand>
    {
        private readonly IMapper _mapper;

        public UpdateSliderCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSliderCommand request, CancellationToken cancellationToken)
        {
            var slider = await Slider.GetAsync(request.Id, cancellationToken);
            _mapper.Map(request, slider);
            await slider.DomainService.SetImage(cancellationToken);
            await slider.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
