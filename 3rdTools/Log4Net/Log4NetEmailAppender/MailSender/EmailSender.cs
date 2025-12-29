using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Log4NetEmailAppender.MailSender;

public class EmailSender:IDisposable
{
    private string _emailSubject;

    private string _emailTitle;
    
    public string EmailServer { get; set; }
    public int EmailPort { get; set; }
    public string EmailUser { get; set; }
    public string EmailPassword { get; set; }
    public string EmailSendTo { get; set; }
    public string EmailSendCC { get; set; }
    public string EmailSendFrom { get; set; }
    public string EmailSendFromName { get; set; }
    public bool UseDefaultCredentials { get; set; }
    public EmailSender(string emailSubject, string emailTitle)
    {
        _emailSubject = emailSubject;
        _emailTitle = emailTitle;
    }

    private async Task<bool> SendMail(string sSubject, string sMessage, MailPriority iPriority = MailPriority.Normal)
    {
        try
        {
            SmtpClient smtpClient = new ();
            MailMessage message = new ();

            MailAddress fromAddress = new (EmailSendFrom, EmailSendFromName);

            //You can have multiple emails separated by ;
            string[] sEmailTo = Regex.Split(EmailSendTo, ";");
            string[] sEmailCC = Regex.Split(EmailSendCC, ";");

            smtpClient.Host = EmailServer;
            smtpClient.Port = EmailPort;

            //Set this property to true when this SmtpClient object should, if requested by the server, authenticate using the default credentials of the currently logged on user
            //smtpClient.UseDefaultCredentials = true;
            if (UseDefaultCredentials)
            {
                smtpClient.UseDefaultCredentials = true;
            }
            else
            {
                System.Net.NetworkCredential myCredentials =
               new System.Net.NetworkCredential(EmailUser, EmailPassword);
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
            message.IsBodyHtml = false;
            message.Body = sMessage;

            await smtpClient.SendMailAsync(message);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task SendBulkAsync(IEnumerable<InnerBulkOperation> bulk)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"{_emailTitle}");
        sb.AppendLine("");
        foreach (InnerBulkOperation bulkOperation in bulk)
        {
            foreach(var param in bulkOperation.OperationParams)
            {
                sb.AppendLine($"{param.Key} :=> {param.Value}");
            }
            sb.AppendLine("");
            sb.AppendLine("************************************************************************************************************");
            sb.AppendLine("");
        }
        await SendMail(_emailSubject, sb.ToString());
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
        }

    }
}
