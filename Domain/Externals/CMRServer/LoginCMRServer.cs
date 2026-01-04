namespace Domain.Externals.CMRServer
{
    public class LoginCMRServer : CMRRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
