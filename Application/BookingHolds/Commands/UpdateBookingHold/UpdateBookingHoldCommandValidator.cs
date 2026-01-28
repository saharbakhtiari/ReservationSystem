using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.BookingHolds.Commands.UpdateBookingHold
{
    public class UpdateBookingHoldCommandValidator : AbstractValidator<UpdateBookingHoldCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateBookingHoldCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
