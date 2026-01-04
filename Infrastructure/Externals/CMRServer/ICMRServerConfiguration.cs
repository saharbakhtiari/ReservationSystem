namespace Infrastructure.Externals.CMRServer
{
    public interface ICMRServerConfiguration
    {
        string EndpointAddress { get; set; }
        string LoginApi { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
