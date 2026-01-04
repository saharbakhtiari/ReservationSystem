namespace Infrastructure.Externals.CMRServer
{
    public class CMRServerConfiguration : ICMRServerConfiguration
    {
        public string EndpointAddress { get; set; }
        public string LoginApi { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}