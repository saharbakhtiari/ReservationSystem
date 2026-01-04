namespace Domain.Externals.MavaServer
{
    public class RefreshTokenMavaServer : MavaRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
