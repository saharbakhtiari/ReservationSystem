using Application.Spaces.Commands.UpdateSpace;
using AutoMapper;
using Domain.Spaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Spaces.Commands.UpdateSpace
{
    public class UpdateSpaceCommandHandler : IRequestHandler<UpdateSpaceCommand>
    {
        private readonly IMapper _mapper;

        public UpdateSpaceCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSpaceCommand request, CancellationToken cancellationToken)
        {
            var space = await Space.GetAsync(request.Id, cancellationToken);
            _mapper.Map(request, space);
            await space.DomainService.UpdateImages(cancellationToken);
            await space.DomainService.SetAmenities(request.AmenityIds, cancellationToken);
            await space.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
