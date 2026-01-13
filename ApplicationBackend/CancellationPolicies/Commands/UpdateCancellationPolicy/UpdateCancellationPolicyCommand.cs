using Application.CancellationPolicys.Commands.UpdateCancellationPolicy;
using AutoMapper;
using Domain.CancellationPolicys;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.CancellationPolicys.Commands.UpdateCancellationPolicy
{
    public class UpdateCancellationPolicyCommandHandler : IRequestHandler<UpdateCancellationPolicyCommand>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;


        public UpdateCancellationPolicyCommandHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateCancellationPolicyCommand request, CancellationToken cancellationToken)
        {
            var tariff = await CancellationPolicy.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            _mapper.Map(request, tariff);
            await tariff.DomainService.SetTariff(request.TariffId, cancellationToken);
            await tariff.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
