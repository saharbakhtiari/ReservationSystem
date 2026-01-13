using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.CancellationPolicys.Commands.DeleteCancellationPolicy
{
    public class DeleteCancellationPolicyCommandValidator : AbstractValidator<DeleteCancellationPolicyCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteCancellationPolicyCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
