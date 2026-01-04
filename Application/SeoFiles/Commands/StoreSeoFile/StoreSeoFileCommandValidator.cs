using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.SeoFiles.Commands.StoreSeoFile
{
    public class StoreSeoFileCommandValidator : AbstractValidator<StoreSeoFileCommand>
    {
        private readonly IStringLocalizer _localizer;
        public StoreSeoFileCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Name).NotEmpty().WithMessage(_localizer["File name is empty"]);
            RuleFor(p => p.FileType).NotEmpty().WithMessage(_localizer["File type is empty"]);
            RuleFor(p => p.DataFiles).NotEmpty().WithMessage(_localizer["File is not correct"]);
        }
    }
}
