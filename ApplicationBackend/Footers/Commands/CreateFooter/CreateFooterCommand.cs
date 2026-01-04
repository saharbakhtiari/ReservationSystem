using Application.Footers.Commands.CreateFooter;
using AutoMapper;
using Domain.Footers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Footers.Commands.CreateFooter
{
    public class CreateFooterCommandHandler : IRequestHandler<CreateFooterCommand, long>
    {
        private readonly IMapper _mapper;

        public CreateFooterCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateFooterCommand request, CancellationToken cancellationToken)
        {
            var footer = _mapper.Map<Footer>(request);
            await footer.SaveAsync(cancellationToken);
            return footer.Id;
        }
    }
}
