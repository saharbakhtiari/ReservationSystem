using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.TimeSlots.Commands.CreateTimeSlot
{
    public class CreateTimeSlotCommandValidator : AbstractValidator<CreateTimeSlotCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateTimeSlotCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
          //  RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);
            // RuleFor(p => p.DataFiles).NotEmpty().WithMessage("فایل ارسالی صحیح نیست");
        }
    }
}
