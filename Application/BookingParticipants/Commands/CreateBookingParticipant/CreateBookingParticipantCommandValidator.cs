using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.BookingParticipants.Commands.CreateBookingParticipant
{
    public class CreateBookingParticipantCommandValidator : AbstractValidator<CreateBookingParticipantCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateBookingParticipantCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.FirstName).NotEmpty().WithMessage(_localizer["FirstName is empty"]);
            RuleFor(p => p.LastName).NotEmpty().WithMessage(_localizer["LastName is empty"]);
            RuleFor(p => p.NationalCode).NotEmpty().WithMessage(_localizer["NationalCode is empty"]);
            // RuleFor(p => p.DataFiles).NotEmpty().WithMessage("فایل ارسالی صحیح نیست");
        }
    }
}
