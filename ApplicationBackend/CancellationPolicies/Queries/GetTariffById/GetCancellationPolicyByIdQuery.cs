using Application.CancellationPolicys.Queries.GetCancellationPolicy;
using AutoMapper;
using Domain.CancellationPolicys;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.CancellationPolicys.Queries.GetCancellationPolicy
{
    public class GetCancellationPolicyByIdQueryHandler : IRequestHandler<GetCancellationPolicyByIdQuery, GetCancellationPolicyByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetCancellationPolicyByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<GetCancellationPolicyByIdDto> Handle(GetCancellationPolicyByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await CancellationPolicy.GetIncludedAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            return _mapper.Map<GetCancellationPolicyByIdDto>(item);
        }
    }
}
