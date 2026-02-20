using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Bookings.Commands.AdminRevokeBooking
{
    public class AdminRevokeBookingCommandValidator : AbstractValidator<AdminRevokeBookingCommand>
    {
        private readonly IStringLocalizer _localizer;
        public AdminRevokeBookingCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
