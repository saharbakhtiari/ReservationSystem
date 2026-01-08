using Application.Amenitys.Queries.GetAmenity;
using AutoMapper;
using Domain.Amenitys;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Amenitys.Queries.GetAmenity
{
    public class GetAmenityByIdQueryHandler : IRequestHandler<GetAmenityByIdQuery, GetAmenityByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetAmenityByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<GetAmenityByIdDto> Handle(GetAmenityByIdQuery request, CancellationToken cancellationToken)
        {
            var amenity = await Amenity.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            return _mapper.Map<GetAmenityByIdDto>(amenity);
        }
    }
}
