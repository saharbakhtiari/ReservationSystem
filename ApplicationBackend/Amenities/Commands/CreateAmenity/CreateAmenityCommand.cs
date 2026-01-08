using Application.Amenitys.Commands.CreateAmenity;
using AutoMapper;
using Domain.Amenitys;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Amenitys.Commands.CreateAmenity
{
    public class CreateAmenityCommandHandler : IRequestHandler<CreateAmenityCommand, long>
    {
        private readonly IMapper _mapper;

        public CreateAmenityCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
        {
            var Amenity = _mapper.Map<Amenity>(request);
            await Amenity.SaveAsync(cancellationToken);
            return Amenity.Id;
        }
    }
}
