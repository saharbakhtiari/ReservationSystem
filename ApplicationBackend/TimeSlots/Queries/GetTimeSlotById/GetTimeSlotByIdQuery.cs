using Application.TimeSlots.Queries.GetTimeSlot;
using AutoMapper;
using Domain.TimeSlots;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.TimeSlots.Queries.GetTimeSlot
{
    public class GetTimeSlotByIdQueryHandler : IRequestHandler<GetTimeSlotByIdQuery, GetTimeSlotByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetTimeSlotByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<GetTimeSlotByIdDto> Handle(GetTimeSlotByIdQuery request, CancellationToken cancellationToken)
        {
            var tariff = await TimeSlot.GetIncludedAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            return _mapper.Map<GetTimeSlotByIdDto>(tariff);
        }
    }
}
