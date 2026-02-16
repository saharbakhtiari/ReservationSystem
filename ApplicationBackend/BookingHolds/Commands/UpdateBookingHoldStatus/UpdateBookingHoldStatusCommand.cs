using Application.BookingHolds.Commands.UpdateBookingHoldStatus;
using Domain.BookingHolds;
using Domain.Bookings;
using Domain.Contract.Enums;
using Exceptions;
using Extensions;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
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
            if (request.Status == BookingHoldStatus.Completed)
            {
                var hold = await BookingHold.GetIncludedAsync(request.Id,true, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
                var currency = hold.Details.FirstOrDefault().TimeSlot?.Tariff?.Currency;
                var totalAmount = hold.Details.Sum(a => a.TimeSlot.Tariff.Price);
                //Create Booking
                Booking booking = new()
                {
                    BookingHoldId = hold.Id,
                    Details = hold.Details,
                    Profile = hold.Profile,
                    ConfirmedAt = DateTime.Now,
                    Currency = currency.HasValue ? currency.Value : Currency.None,
                    Status = BookingStatus.Completed,
                    TotalAmount = totalAmount,
                    PolicySnapshot = hold.Details.ToJson(),
                    PriceSnapshot = ""
                };
                await booking.SaveAsync(cancellationToken);
                hold.Status = request.Status;
                await hold.SaveAsync(cancellationToken);
            }
            else
            {
                var hold = await BookingHold.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
                hold.Status = request.Status;
                await hold.SaveAsync(cancellationToken);
            }
               
            return Unit.Value;
        }
    }
}
