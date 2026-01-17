using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateBookingCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
          //  RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);
            // RuleFor(p => p.DataFiles).NotEmpty().WithMessage("فایل ارسالی صحیح نیست");
        }
    }
}
