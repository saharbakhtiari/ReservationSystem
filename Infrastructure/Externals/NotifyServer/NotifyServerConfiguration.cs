namespace Infrastructure.Externals.NotifyServer
{
    public class NotifyServerConfiguration : INotifyServerConfiguration
    {
        public string EndpointAddress { get; set; }
        public string LoginApi { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}