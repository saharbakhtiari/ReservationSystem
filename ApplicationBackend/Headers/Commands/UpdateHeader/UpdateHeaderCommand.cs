using Application.Headers.Commands.UpdateHeader;
using AutoMapper;
using Domain.Headers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Headers.Commands.UpdateHeader
{
    public class UpdateHeaderCommandHandler : IRequestHandler<UpdateHeaderCommand>
    {
        private readonly IMapper _mapper;

        public UpdateHeaderCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateHeaderCommand request, CancellationToken cancellationToken)
        {
            var header = await Header.GetHeaderAsync(request.Id, cancellationToken);
            _mapper.Map(request, header);
            if (request.DataFiles != null)
                header.DomainService.SetFile(request.DataFiles);
            await header.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
