using Application.Tariffs.Commands.CreateTariff;
using AutoMapper;
using Domain.Tariffs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Tariffs.Commands.CreateTariff
{
    public class CreateTariffCommandHandler : IRequestHandler<CreateTariffCommand, long>
    {
        private readonly IMapper _mapper;


        public CreateTariffCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateTariffCommand request, CancellationToken cancellationToken)
        {
            var tariff = _mapper.Map<Tariff>(request);
            await tariff.DomainService.SetSpace(request.SpaceId, cancellationToken);
            await tariff.SaveAsync(cancellationToken);
            return tariff.Id;
        }
    }
}
