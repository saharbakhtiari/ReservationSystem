using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.BookingParticipants.Commands.UpdateBookingParticipant
{
    public class UpdateBookingParticipantCommandValidator : AbstractValidator<UpdateBookingParticipantCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateBookingParticipantCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
            RuleFor(p => p.FirstName).NotEmpty().WithMessage(_localizer["FirstName is empty"]);
            RuleFor(p => p.LastName).NotEmpty().WithMessage(_localizer["LastName is empty"]);
            RuleFor(p => p.NationalCode).NotEmpty().WithMessage(_localizer["NationalCode is empty"]);
        }
    }
}
