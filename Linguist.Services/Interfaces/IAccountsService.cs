using System.Web;

namespace Linguist.Services.Interfaces
{
    public interface IAccountsService
    {
        bool AuthenticateUser(string login, string password);

        string GetHashFromPassword(string password, int salt);

        int ComputeSalt();

        string GetUserName(HttpContext context);

        bool SetPassword(string login, string password);

        string GenerateRestoreCode(string login);
    }
}
