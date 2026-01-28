using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.BookingHolds.Commands.UpdateBookingHoldStatus
{
    public class UpdateBookingHoldStatusCommandValidator : AbstractValidator<UpdateBookingHoldStatusCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateBookingHoldStatusCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
