namespace CustomLoggers.EmailSenders
{
    public class MailSenderConfiguration : IMailSenderConfiguration
    {
        public string DomainName { get; set; }
        public string EmailServer { get; set; }
        public int EmailPort { get; set; }
        public string EmailUser { get; set; }
        public string EmailPassword { get; set; }
        public string EmailSendTo { get; set; }
        public string EmailSendCC { get; set; }
        public string EmailSendFrom { get; set; }
        public string EmailSendFromName { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }
}
