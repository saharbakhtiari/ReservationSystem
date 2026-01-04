using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.SeoFiles.Commands.DeleteSeoFile
{
    public class DeleteSeoFileCommandValidator : AbstractValidator<DeleteSeoFileCommand>
    {
        private readonly IStringLocalizer _localizer;


        public DeleteSeoFileCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.FileGuid).NotEmpty().WithMessage(_localizer["File is not selected"]);
        }
    }
}
