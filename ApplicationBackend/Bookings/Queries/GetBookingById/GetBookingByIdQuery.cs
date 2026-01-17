using Application.Bookings.Queries.GetBooking;
using AutoMapper;
using Domain.Bookings;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Bookings.Queries.GetBooking
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, GetBookingByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetBookingByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<GetBookingByIdDto> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var tariff = await Booking.GetIncludedAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            return _mapper.Map<GetBookingByIdDto>(tariff);
        }
    }
}
