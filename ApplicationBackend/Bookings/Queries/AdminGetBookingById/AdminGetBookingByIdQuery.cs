using Application.Bookings.Queries.AdminGetBooking;
using AutoMapper;
using Domain.Bookings;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Bookings.Queries.AdminGetBooking
{
    public class AdminGetBookingByIdQueryHandler : IRequestHandler<AdminGetBookingByIdQuery, AdminGetBookingByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public AdminGetBookingByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<AdminGetBookingByIdDto> Handle(AdminGetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var booking = await Booking.GetIncludedAsync(request.Id, true, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            return _mapper.Map<AdminGetBookingByIdDto>(booking);
        }
    }
}
