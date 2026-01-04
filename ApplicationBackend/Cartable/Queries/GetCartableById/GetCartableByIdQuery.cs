using Application.Cartable.Queries.GetCartableById;
using AutoMapper;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Cartable.Queries.GetCartableById
{
    public class GetCartableByIdQueryHandler : IRequestHandler<GetCartableByIdQuery, CartableDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetCartableByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<CartableDto> Handle(GetCartableByIdQuery request, CancellationToken cancellationToken)
        {
            var NotificationAuthoritiy = await Domain.Cartables.Cartable.GetCartableByIdAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["NotificationAuthoritiy not found"]);
            return _mapper.Map<CartableDto>(NotificationAuthoritiy);
        }
    }
}
