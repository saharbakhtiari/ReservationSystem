namespace GhasedakSmsService
{
    public interface IAppSettingCMIX
    {
        public string ApiBase_Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Credential { get; set; }

        public string SMS_ApiKey { get; set; }
        public string SMS_SenderNumber { get; set; }
    }
}
