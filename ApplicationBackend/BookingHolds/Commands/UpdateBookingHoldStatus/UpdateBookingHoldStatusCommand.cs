using Application.BookingHolds.Commands.UpdateBookingHoldStatus;
using Domain.BookingHolds;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingHolds.Commands.UpdateBookingHold
{
    public class UpdateBookingHoldStatusCommandHandler : IRequestHandler<UpdateBookingHoldStatusCommand>
    {
        private readonly IStringLocalizer _localizer;


        public UpdateBookingHoldStatusCommandHandler(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateBookingHoldStatusCommand request, CancellationToken cancellationToken)
        {
            var tariff = await BookingHold.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            tariff.Status = request.Status;
            await tariff.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
