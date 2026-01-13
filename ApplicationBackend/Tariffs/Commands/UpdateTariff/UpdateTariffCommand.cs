using Application.Tariffs.Commands.UpdateTariff;
using AutoMapper;
using Domain.Tariffs;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Tariffs.Commands.UpdateTariff
{
    public class UpdateTariffCommandHandler : IRequestHandler<UpdateTariffCommand>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;


        public UpdateTariffCommandHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateTariffCommand request, CancellationToken cancellationToken)
        {
            var tariff = await Tariff.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            _mapper.Map(request, tariff);
            await tariff.DomainService.SetSpace(request.SpaceId, cancellationToken);
            await tariff.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
