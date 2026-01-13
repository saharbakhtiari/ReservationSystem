using Application.CancellationPolicys.Commands.CreateCancellationPolicy;
using AutoMapper;
using Domain.CancellationPolicys;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.CancellationPolicys.Commands.CreateCancellationPolicy
{
    public class CreateCancellationPolicyCommandHandler : IRequestHandler<CreateCancellationPolicyCommand, long>
    {
        private readonly IMapper _mapper;


        public CreateCancellationPolicyCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateCancellationPolicyCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<CancellationPolicy>(request);
            await item.DomainService.SetTariff(request.TariffId, cancellationToken);
            await item.SaveAsync(cancellationToken);
            return item.Id;
        }
    }
}
