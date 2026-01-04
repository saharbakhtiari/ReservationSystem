namespace Application.Common.Notifications
{
    public interface INotificationConfiguration
    {
        string NotificationSenderNumber { get; set; }
        string NotificationSenderEmail { get; set; }
    }
}