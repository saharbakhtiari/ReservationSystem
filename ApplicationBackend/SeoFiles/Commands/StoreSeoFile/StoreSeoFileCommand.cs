using AutoMapper;
using Domain.SeoFiles;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SeoFiles.Commands.StoreSeoFile
{
    public class StoreSeoFileCommandHandler : IRequestHandler<StoreSeoFileCommand, StoreSeoFileDto>
    {
        private readonly IMapper _mapper;

        public StoreSeoFileCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<StoreSeoFileDto> Handle(StoreSeoFileCommand request, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<SeoFile>(request);
            await file.DomainService.StoreFile(cancellationToken);
            return new()
            {
                FileGuid = file.FileGuid,
                Id = file.Id,
                Url = $"/file/{file.Id}"
            };
        }
    }
}
