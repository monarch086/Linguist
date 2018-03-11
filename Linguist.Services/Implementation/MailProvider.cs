using System.Net.Mail;
using Linguist.Services.Interfaces;

namespace Linguist.Services.Implementation
{
    public class MailProvider : IMailProvider
    {
        private string Host => "smtp.gmail.com";
        private int Port => 587;
        private string UserName => "linguistlabs@gmail.com";
        private string Password => "trh1829G_k";

        public SmtpClient GetSmtpClient()
        {
            var smtpClient =
                new SmtpClient(Host, Port)
                {
                    Credentials = new System.Net.NetworkCredential(UserName, Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                };

            return smtpClient;
        }

        public string SiteMailAddress => "linguistlabs@gmail.com";
    }
}
