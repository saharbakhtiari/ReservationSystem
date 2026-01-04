using Application.Cartable.Commands.UpdateCartable;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Cartable.Commands.UpdateCartable
{
    public class UpdateCartableCommandHandler : IRequestHandler<UpdateCartableCommand>
    {
        private readonly IMapper _mapper;

        public UpdateCartableCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCartableCommand request, CancellationToken cancellationToken)
        {
            var item = await Domain.Cartables.Cartable.GetCartableByIdAsync(request.Id, cancellationToken);
            _mapper.Map(request, item);
            await item.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
