using Application.Spaces.Queries.GetSpace;
using AutoMapper;
using Domain.Spaces;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Spaces.Queries.GetSpace
{
    public class GetSpaceByIdQueryHandler : IRequestHandler<GetSpaceByIdQuery, GetSpaceByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetSpaceByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<GetSpaceByIdDto> Handle(GetSpaceByIdQuery request, CancellationToken cancellationToken)
        {
            var space = await Space.GetIncludedAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Space not found"]);
            return _mapper.Map<GetSpaceByIdDto>(space);
        }
    }
}
