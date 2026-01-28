using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Bookings.Queries.AdminGetBooking
{
    public class AdminGetBookingByIdQueryValidator : AbstractValidator<AdminGetBookingByIdQuery>
    {
        private readonly IStringLocalizer _localizer;
        public AdminGetBookingByIdQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
