using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.AdminBookingHolds.Queries.GetAdminBookingHold
{
    public class GetAdminBookingHoldByIdQueryValidator : AbstractValidator<GetAdminBookingHoldByIdQuery>
    {
        private readonly IStringLocalizer _localizer;
        public GetAdminBookingHoldByIdQueryValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
