using Application.AdminBookingHolds.Queries.GetAdminBookingHold;
using AutoMapper;
using Domain.BookingHolds;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingHolds.Queries.AdminGetBookingHold
{
    public class AdminGetBookingHoldByIdQueryHandler : IRequestHandler<AdminGetBookingHoldByIdQuery, AdminGetBookingHoldByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public AdminGetBookingHoldByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<AdminGetBookingHoldByIdDto> Handle(AdminGetBookingHoldByIdQuery request, CancellationToken cancellationToken)
        {
            var hold = await BookingHold.GetIncludedAsync(request.Id,true, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            return _mapper.Map<AdminGetBookingHoldByIdDto>(hold);
        }
    }
}
