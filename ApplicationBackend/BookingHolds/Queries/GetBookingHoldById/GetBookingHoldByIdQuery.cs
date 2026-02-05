using Application.BookingHolds.Queries.GetBookingHold;
using AutoMapper;
using Domain.BookingHolds;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingHolds.Queries.GetBookingHold
{
    public class GetBookingHoldByIdQueryHandler : IRequestHandler<GetBookingHoldByIdQuery, GetBookingHoldByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public GetBookingHoldByIdQueryHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<GetBookingHoldByIdDto> Handle(GetBookingHoldByIdQuery request, CancellationToken cancellationToken)
        {
            var hold = await BookingHold.GetIncludedAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            return _mapper.Map<GetBookingHoldByIdDto>(hold);
        }
    }
}
