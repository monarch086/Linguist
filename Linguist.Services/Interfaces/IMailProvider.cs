using System.Net.Mail;

namespace Linguist.Services.Interfaces
{
    public interface IMailProvider
    {
        SmtpClient GetSmtpClient();

        string SiteMailAddress { get; }
    }
}
