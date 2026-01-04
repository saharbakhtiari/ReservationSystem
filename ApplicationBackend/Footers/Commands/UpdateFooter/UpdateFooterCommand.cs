using Application.Footers.Commands.UpdateFooter;
using AutoMapper;
using Domain.Footers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Footers.Commands.UpdateFooter
{
    public class UpdateFooterCommandHandler : IRequestHandler<UpdateFooterCommand>
    {
        private readonly IMapper _mapper;

        public UpdateFooterCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFooterCommand request, CancellationToken cancellationToken)
        {
            var footer = await Footer.GetFooterAsync(request.Id, cancellationToken);
            _mapper.Map(request, footer);
            await footer.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
