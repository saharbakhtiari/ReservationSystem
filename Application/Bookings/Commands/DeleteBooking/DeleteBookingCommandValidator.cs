using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteBookingCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
