using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomLoggers.EmailSenders
{
    public class EmailSender : IEmailSender
    {
        private IMailSenderConfiguration _senderConfiguration;
        public EmailSender(IMailSenderConfiguration senderConfiguration)
        {
            try
            {
                _senderConfiguration = senderConfiguration;
            }
            catch
            {

            }
        }

        public async Task<bool> SendMail(string sSubject, string sMessage, MailPriority iPriority = MailPriority.Normal)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();

                MailAddress fromAddress = new MailAddress(_senderConfiguration.EmailSendFrom, _senderConfiguration.EmailSendFromName);

                //You can have multiple emails separated by ;
                string[] sEmailTo = Regex.Split(_senderConfiguration.EmailSendTo, ";");
                string[] sEmailCC = Regex.Split(_senderConfiguration.EmailSendCC, ";");

                smtpClient.Host = _senderConfiguration.EmailServer;
                smtpClient.Port = _senderConfiguration.EmailPort;

                //Set this property to true when this SmtpClient object should, if requested by the server, authenticate using the default credentials of the currently logged on user
                //smtpClient.UseDefaultCredentials = true;
                if (_senderConfiguration.UseDefaultCredentials)
                {
                    smtpClient.UseDefaultCredentials = true;
                }
                else
                {
                    System.Net.NetworkCredential myCredentials =
                   new System.Net.NetworkCredential(_senderConfiguration.EmailUser, _senderConfiguration.EmailPassword);
                    smtpClient.Credentials = myCredentials;
                }

                message.From = fromAddress;

                if (sEmailTo != null)
                {
                    for (int i = 0; i < sEmailTo.Length; ++i)
                    {
                        if (sEmailTo[i] != null && sEmailTo[i] != "")
                        {
                            message.To.Add(sEmailTo[i]);
                        }
                    }
                }

                if (sEmailCC != null)
                {
                    for (int i = 0; i < sEmailCC.Length; ++i)
                    {
                        if (sEmailCC[i] != null && sEmailCC[i] != "")
                        {
                            message.To.Add(sEmailCC[i]);
                        }
                    }
                }
                message.Priority = iPriority;

                message.Subject = sSubject;
                message.IsBodyHtml = true;
                message.Body = sMessage;

                await smtpClient.SendMailAsync(message);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
