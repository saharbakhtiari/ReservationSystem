using Application.Footers.Queries.GetFooter;
using AutoMapper;
using Domain.Footers;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Footers.Queries.GetFooter
{
    public class GetFooterQueryHandler : IRequestHandler<GetFooterQuery, FooterDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetFooterQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<FooterDto> Handle(GetFooterQuery request, CancellationToken cancellationToken)
        {
            var footer = await Footer.GetFooterAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Footer not found"]);
            return _mapper.Map<FooterDto>(footer);
        }
    }
}
