using Application.SeoFiles.Queries.GetSeoFileById;
using AutoMapper;
using Domain.SeoFiles;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.SeoFiles.Queries.GetSeoFileById
{
    public class GetSeoFileByIdQueryHandler : IRequestHandler<GetSeoFileByIdQuery, SeoFileDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetSeoFileByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<SeoFileDto> Handle(GetSeoFileByIdQuery request, CancellationToken cancellationToken)
        {
            var annoncement = await SeoFile.GetFileAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["SeoFile not found"]);
            return _mapper.Map<SeoFileDto>(annoncement);
        }
    }
}
