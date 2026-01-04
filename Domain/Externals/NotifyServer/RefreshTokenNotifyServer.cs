namespace Domain.Externals.NotifyServer
{
    public class RefreshTokenNotifyServer : Notification
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
