using Application.Spaces.Commands.CreateSpace;
using AutoMapper;
using Domain.Spaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Spaces.Commands.CreateSpace
{
    public class CreateSpaceCommandHandler : IRequestHandler<CreateSpaceCommand, long>
    {
        private readonly IMapper _mapper;

        public CreateSpaceCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateSpaceCommand request, CancellationToken cancellationToken)
        {
            var space = _mapper.Map<Space>(request);
            await space.DomainService.CreateImages(cancellationToken);
            await space.DomainService.SetAmenities(request.AmenityIds, cancellationToken);
            await space.SaveAsync(cancellationToken);
            return space.Id;
        }
    }
}
