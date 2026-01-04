using Application.Headers.Queries.GetHeader;
using AutoMapper;
using Domain.Headers;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Headers.Queries.GetHeader
{
    public class GetHeaderQueryHandler : IRequestHandler<GetHeaderQuery, HeaderDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetHeaderQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<HeaderDto> Handle(GetHeaderQuery request, CancellationToken cancellationToken)
        {
            var header = await Header.GetHeaderAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Header not found"]);
            return _mapper.Map<HeaderDto>(header);
        }
    }
}
