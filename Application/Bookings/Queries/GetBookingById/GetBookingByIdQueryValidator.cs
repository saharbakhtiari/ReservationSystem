using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Bookings.Queries.GetBooking
{
    public class GetBookingByIdQueryValidator : AbstractValidator<GetBookingByIdQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetBookingByIdQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
