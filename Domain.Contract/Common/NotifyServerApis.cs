namespace Domain.Contract.Common
{
    public static class NotifyServerApis
    {
        public static readonly string DeleteSourceEmail = "SourceEmail/delete";
        public static readonly string CreateEmailNotify = "EmailNotify/create";
        public static readonly string DeleteEmailNotify = "EmailNotify/delete";
        public static readonly string SendNotification = "Notification/sendnotification";
        public static readonly string LoginNotify = "Users/login";
        public static readonly string RefreshTokenNotify = "Users/refreshtoken";
        public static readonly string CreateSourceEmail = "SourceEmail/create";
        public static readonly string CheckEmailNotify = "EmailNotify/check";
        public static readonly string CheckSmsNotify = "SmsNotify/check";
        public static readonly string CreateSmsNotify = "SmsNotify/create";
        public static readonly string DeleteSmsNotify = "SmsNotify/delete";
        public static readonly string StartNotification = "Notification/start";
        public static readonly string StopNotification = "Notification/stop";
        public static readonly string GetNotificationStatus = "Notification/getstatus";
    }
}
