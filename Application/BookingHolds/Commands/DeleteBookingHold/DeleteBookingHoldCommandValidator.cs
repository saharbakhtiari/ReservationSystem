using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.BookingHolds.Commands.DeleteBookingHold
{
    public class DeleteBookingHoldCommandValidator : AbstractValidator<DeleteBookingHoldCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteBookingHoldCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
