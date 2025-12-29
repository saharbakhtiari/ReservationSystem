namespace CustomLoggers.EmailSenders;

public interface IMailSenderConfiguration
{
    string DomainName { get; set; }
    string EmailPassword { get; set; }
    int EmailPort { get; set; }
    string EmailSendCC { get; set; }
    string EmailSendFrom { get; set; }
    string EmailSendFromName { get; set; }
    string EmailSendTo { get; set; }
    string EmailServer { get; set; }
    string EmailUser { get; set; }
    bool UseDefaultCredentials { get; set; }
}
