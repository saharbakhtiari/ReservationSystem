namespace SmsService.Contract
{
    public interface ISMSService
    {

        public Task<bool> Send(string phoneNumber, string msg, CancellationToken cancellation);
    }
}