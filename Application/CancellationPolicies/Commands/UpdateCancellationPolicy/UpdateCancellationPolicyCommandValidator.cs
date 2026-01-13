using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.CancellationPolicys.Commands.UpdateCancellationPolicy
{
    public class UpdateCancellationPolicyCommandValidator : AbstractValidator<UpdateCancellationPolicyCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateCancellationPolicyCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
