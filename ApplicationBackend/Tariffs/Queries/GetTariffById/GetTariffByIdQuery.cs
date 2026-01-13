using Application.Tariffs.Queries.GetTariff;
using AutoMapper;
using Domain.Tariffs;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Tariffs.Queries.GetTariff
{
    public class GetTariffByIdQueryHandler : IRequestHandler<GetTariffByIdQuery, GetTariffByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetTariffByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<GetTariffByIdDto> Handle(GetTariffByIdQuery request, CancellationToken cancellationToken)
        {
            var tariff = await Tariff.GetIncludedAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            return _mapper.Map<GetTariffByIdDto>(tariff);
        }
    }
}
