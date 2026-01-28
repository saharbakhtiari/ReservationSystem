using Application.BookingHolds.Commands.UpdateBookingHold;
using AutoMapper;
using Domain.BookingHolds;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingHolds.Commands.UpdateBookingHold
{
    public class UpdateBookingHoldCommandHandler : IRequestHandler<UpdateBookingHoldCommand>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;


        public UpdateBookingHoldCommandHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateBookingHoldCommand request, CancellationToken cancellationToken)
        {
            var tariff = await BookingHold.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            _mapper.Map(request, tariff);
            await tariff.DomainService.SetSpace(request.SpaceId, cancellationToken);
            await tariff.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
