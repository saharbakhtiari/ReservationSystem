using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.TimeSlots.Commands.DeleteTimeSlot
{
    public class DeleteTimeSlotCommandValidator : AbstractValidator<DeleteTimeSlotCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteTimeSlotCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
