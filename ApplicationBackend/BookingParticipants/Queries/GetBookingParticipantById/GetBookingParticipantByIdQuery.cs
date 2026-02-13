using Application.BookingParticipants.Queries.GetBookingParticipant;
using AutoMapper;
using Domain.BookingParticipants;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingParticipants.Queries.GetBookingParticipant
{
    public class GetBookingParticipantByIdQueryHandler : IRequestHandler<GetBookingParticipantByIdQuery, GetBookingParticipantByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetBookingParticipantByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<GetBookingParticipantByIdDto> Handle(GetBookingParticipantByIdQuery request, CancellationToken cancellationToken)
        {
            var tariff = await BookingParticipant.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            return _mapper.Map<GetBookingParticipantByIdDto>(tariff);
        }
    }
}
