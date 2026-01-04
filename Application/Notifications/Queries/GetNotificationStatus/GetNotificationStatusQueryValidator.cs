using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Notifications.Queries.GetNotificationStatus
{
    public class GetNotificationStatusQueryValidator : AbstractValidator<GetNotificationStatusQuery>
    {
        public GetNotificationStatusQueryValidator()
        {
        }
    }
}
