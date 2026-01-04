namespace Domain.Externals.CMRServer
{
    public class RefreshTokenCMRServer : CMRRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
