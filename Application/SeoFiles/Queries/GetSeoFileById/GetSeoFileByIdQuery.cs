using Application.SeoFiles.Queries.GetSeoFileById;
using MediatR;

namespace Application_Backend.SeoFiles.Queries.GetSeoFileById
{
    public class GetSeoFileByIdQuery : IRequest<SeoFileDto>
    {
        public long Id { get; set; }
    }
}
