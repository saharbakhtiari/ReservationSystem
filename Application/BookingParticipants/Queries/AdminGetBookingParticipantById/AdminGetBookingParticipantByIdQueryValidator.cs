using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.BookingParticipants.Queries.AdminGetBookingParticipant
{
    public class AdminGetBookingParticipantByIdQueryValidator : AbstractValidator<AdminGetBookingParticipantByIdQuery>
    {
        private readonly IStringLocalizer _localizer;
        public AdminGetBookingParticipantByIdQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
