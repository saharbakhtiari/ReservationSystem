using Application.Cartable.Commands.CreateCartable;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Cartable.Commands.CreateCartable
{
    public class CreateCartableCommandHandler : IRequestHandler<CreateCartableCommand, long>
    {
        private readonly IMapper _mapper;

        public CreateCartableCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateCartableCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Domain.Cartables.Cartable>(request);
            await item.SaveAsync(cancellationToken);
            return item.Id;
        }
    }
}
