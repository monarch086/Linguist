using System.Text;
using Linguist.Services.Interfaces;
using System.Net.Mail;

namespace Linguist.Services.Implementation
{
    public class MailService : IMailService
    {
        private readonly IMailProvider _mailProvider;

        public MailService(IMailProvider mailProvider)
        {
            _mailProvider = mailProvider;
        }

        public void SendMail(string mailAddress, string text, string subject)
        {
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(_mailProvider.SiteMailAddress, "Linguist Labs"),
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8,
                Subject = subject,
                Body = text
            };

            mail.To.Add(new MailAddress(mailAddress));

            _mailProvider.GetSmtpClient().Send(mail);
        }
    }
}
