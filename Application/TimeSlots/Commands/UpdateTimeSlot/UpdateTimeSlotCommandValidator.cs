using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.TimeSlots.Commands.UpdateTimeSlot
{
    public class UpdateTimeSlotCommandValidator : AbstractValidator<UpdateTimeSlotCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateTimeSlotCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
