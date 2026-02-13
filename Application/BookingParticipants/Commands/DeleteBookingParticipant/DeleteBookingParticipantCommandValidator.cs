using Application.BookingParticipants.Commands.DeleteBookingParticipant;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.BookingParticipantParticipants.Commands.DeleteBookingParticipantParticipant
{
    public class DeleteBookingParticipantCommandValidator : AbstractValidator<DeleteBookingParticipantCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteBookingParticipantCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
