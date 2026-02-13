using Application.BookingParticipants.Queries.AdminGetBookingParticipant;
using AutoMapper;
using Domain.BookingParticipants;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingParticipants.Queries.AdminGetBookingParticipant
{
    public class AdminGetBookingParticipantByIdQueryHandler : IRequestHandler<AdminGetBookingParticipantByIdQuery, AdminGetBookingParticipantByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public AdminGetBookingParticipantByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<AdminGetBookingParticipantByIdDto> Handle(AdminGetBookingParticipantByIdQuery request, CancellationToken cancellationToken)
        {
            var booking = await BookingParticipant.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            return _mapper.Map<AdminGetBookingParticipantByIdDto>(booking);
        }
    }
}
