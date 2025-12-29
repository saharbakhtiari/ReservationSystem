using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateBookCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
            RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);
        }
    }
}
