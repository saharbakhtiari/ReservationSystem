using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Bookings.Commands.RevokeBooking
{
    public class RevokeBookingCommandValidator : AbstractValidator<RevokeBookingCommand>
    {
        private readonly IStringLocalizer _localizer;
        public RevokeBookingCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
