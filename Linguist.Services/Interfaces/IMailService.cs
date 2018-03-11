namespace Linguist.Services.Interfaces
{
    public interface IMailService
    {
        void SendMail(string mail, string text, string subject);
    }
}
