namespace Domain.Externals.MavaServer
{
    public class LoginMavaServer : MavaRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
