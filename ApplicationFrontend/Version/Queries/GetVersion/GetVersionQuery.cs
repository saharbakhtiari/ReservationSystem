using Application_Frontend.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.Version.Queries.GetVersion
{
    public class GetVersionQuery : IRequest<string>
    {
    }

    public class GetVersionQueryHandler : IRequestHandler<GetVersionQuery, string>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;

        public GetVersionQueryHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }
        public async Task<string> Handle(GetVersionQuery request, CancellationToken cancellationToken)
        {
            var version = await _clientRequestHandler.GetAsync<string>(ApiAddress.GetVersion);
            return version;
        }
    }
}