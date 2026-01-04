using Application.Common.Notifications;

namespace Application_Backend.Common.Notifications;

public class NotificationConfiguration : INotificationConfiguration
{
    public string NotificationSenderNumber { get; set; }
    public string NotificationSenderEmail { get; set; }
}

