using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.AdminBookingHolds.Queries.GetAdminBookingHold
{
    public class AdminGetBookingHoldByIdQueryValidator : AbstractValidator<AdminGetBookingHoldByIdQuery>
    {
        private readonly IStringLocalizer _localizer;
        public AdminGetBookingHoldByIdQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
