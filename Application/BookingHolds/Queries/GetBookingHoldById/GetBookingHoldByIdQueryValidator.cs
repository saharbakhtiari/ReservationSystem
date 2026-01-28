using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.BookingHolds.Queries.GetBookingHold
{
    public class GetBookingHoldByIdQueryValidator : AbstractValidator<GetBookingHoldByIdQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetBookingHoldByIdQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
