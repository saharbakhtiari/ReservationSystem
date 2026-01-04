namespace Infrastructure.Externals.MavaServer
{
    public interface IMavaServerConfiguration
    {
        string EndpointAddress { get; set; }
        string LoginApi { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
