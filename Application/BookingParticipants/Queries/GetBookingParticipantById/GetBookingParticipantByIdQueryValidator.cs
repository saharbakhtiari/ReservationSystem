using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.BookingParticipants.Queries.GetBookingParticipant
{
    public class GetBookingParticipantByIdQueryValidator : AbstractValidator<GetBookingParticipantByIdQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetBookingParticipantByIdQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
