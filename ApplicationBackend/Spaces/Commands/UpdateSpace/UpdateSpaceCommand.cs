using Application.Spaces.Commands.UpdateSpace;
using AutoMapper;
using Domain.Spaces;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Spaces.Commands.UpdateSpace
{
    public class UpdateSpaceCommandHandler : IRequestHandler<UpdateSpaceCommand>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public UpdateSpaceCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSpaceCommand request, CancellationToken cancellationToken)
        {
            var space = await Space.GetIncludedAmenityAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            _mapper.Map(request, space);
            await space.DomainService.UpdateImages(request.UnChangedGalleryImageIds, cancellationToken);
            await space.DomainService.SetAmenities(request.AmenityIds, cancellationToken);
            await space.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
