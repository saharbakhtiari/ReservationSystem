using System.Net.Mail;
using System.Threading.Tasks;

namespace CustomLoggers.EmailSenders;

public interface IEmailSender
{
    Task<bool> SendMail(string sSubject, string sMessage, MailPriority iPriority = MailPriority.Normal);
}
