using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Notifications.Commands.StartNotification
{
    public class StarNotificationCommandValidator : AbstractValidator<StarNotificationCommand>
    {
        public StarNotificationCommandValidator()
        {
        }
    }
}
