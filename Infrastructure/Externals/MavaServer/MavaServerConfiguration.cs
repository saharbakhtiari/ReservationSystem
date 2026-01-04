namespace Infrastructure.Externals.MavaServer
{
    public class MavaServerConfiguration : IMavaServerConfiguration
    {
        public string EndpointAddress { get; set; }
        public string LoginApi { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}