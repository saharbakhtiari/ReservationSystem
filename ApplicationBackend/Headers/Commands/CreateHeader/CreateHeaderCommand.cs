using Application.Headers.Commands.CreateHeader;
using AutoMapper;
using Domain.Headers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Headers.Commands.CreateHeader
{
    public class CreateHeaderCommandHandler : IRequestHandler<CreateHeaderCommand, long>
    {
        private readonly IMapper _mapper;

        public CreateHeaderCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateHeaderCommand request, CancellationToken cancellationToken)
        {
            var header = _mapper.Map<Header>(request);
            header.DomainService.SetFile(request.DataFiles);
            await header.SaveAsync(cancellationToken);
            return header.Id;
        }
    }
}
