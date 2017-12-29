namespace Linguist.Services.Interfaces
{
    public interface IAccountsService
    {
        bool AuthenticateUser(string login, string password);

        string GetHashFromPassword(string password, int salt);

        int ComputeSalt();
    }
}
