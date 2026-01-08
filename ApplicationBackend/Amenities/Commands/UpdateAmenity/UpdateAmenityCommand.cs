using Application.Amenitys.Commands.UpdateAmenity;
using AutoMapper;
using Domain.Amenitys;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Amenitys.Commands.UpdateAmenity
{
    public class UpdateAmenityCommandHandler : IRequestHandler<UpdateAmenityCommand>
    {
        private readonly IMapper _mapper;

        public UpdateAmenityCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
        {
            var amenity = await Amenity.GetAsync(request.Id, cancellationToken);
            _mapper.Map(request, amenity);
            await amenity.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
