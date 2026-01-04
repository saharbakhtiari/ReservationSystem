namespace Infrastructure.Externals.NotifyServer
{
    public interface INotifyServerConfiguration
    {
        string EndpointAddress { get; set; }
        string LoginApi { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
