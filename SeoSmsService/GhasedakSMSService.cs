using Exceptions;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using RestSharp;
using SmsService.Contract;

namespace GhasedakSmsService
{
    public class GhasedakSMSService : ISMSService
    {
        private readonly IAppSettingCMIX _appSetting;
        private readonly IStringLocalizer _localizer;

        public GhasedakSMSService(IAppSettingCMIX appSetting, IStringLocalizer localizer)
        {
            _appSetting = appSetting;
            _localizer = localizer;
        }
        public async Task<bool> Send(string phoneNumber, string msg, string checkingids)
        {

            RestClientOptions options = new RestClientOptions();
            options.BaseUrl = new Uri("http://api.ghasedaksms.com/v2/sms/send/simple");
            options.MaxTimeout = -1;
            var client = new RestClient(options);
            var request = new RestRequest();
            request.AddHeader("apikey", _appSetting.SMS_ApiKey);

            request.AddParameter("receptor", phoneNumber);
            request.AddParameter("message", msg);
            request.AddParameter("checkingids", checkingids);
            request.AddParameter("sender", _appSetting.SMS_SenderNumber);

            RestResponse response = await client.PostAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var tokenResponse = JsonConvert.DeserializeObject<SmsResponse>(response.Content);
                if (tokenResponse.result.ToUpper().StartsWith("success"))
                    return true;

            }
            return false;


        }
        //ToDo
        public async Task<bool> Send(string phoneNumber, string msg,CancellationToken cancellation)
        {
            var x = _appSetting.SMS_SenderNumber;
            var smsMessage = $"پایگاه قوانین و مقررات بازار سرمایه\n کد تایید : {msg}";
            RestClientOptions options = new RestClientOptions();
            options.BaseUrl = new Uri(_appSetting.ApiBase_Url);
            options.MaxTimeout = -1;
            var client = new RestClient(options);
            var request = new RestRequest();
            request.AddHeader("apikey", _appSetting.SMS_ApiKey);
            request.AddParameter("receptor", phoneNumber);
            request.AddParameter("message", smsMessage);
            request.AddParameter("sender", _appSetting.SMS_SenderNumber);
            try
            {
                RestResponse response = await client.PostAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var tokenResponse = JsonConvert.DeserializeObject<SmsResponse>(response.Content);
                    if (tokenResponse.result.StartsWith("success"))
                        return true;

                }
            }
            catch (Exception ex)
            {
                throw new GhasedakException(_localizer["Send sms error from distination service"], ex);
            }
           
            return false;


        }
    }
}
