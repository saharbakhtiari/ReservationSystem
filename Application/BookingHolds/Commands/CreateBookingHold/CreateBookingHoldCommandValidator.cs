using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.BookingHolds.Commands.CreateBookingHold
{
    public class CreateBookingHoldCommandValidator : AbstractValidator<CreateBookingHoldCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateBookingHoldCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
          //  RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);
            // RuleFor(p => p.DataFiles).NotEmpty().WithMessage("فایل ارسالی صحیح نیست");
        }
    }
}
