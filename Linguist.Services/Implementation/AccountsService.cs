using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Linguist.DataLayer.Model;
using Linguist.DataLayer.Repositories;
using Linguist.Services.Interfaces;
using System.Web.Security;

namespace Linguist.Services.Implementation
{
    public class AccountsService : IAccountsService
    {
        private readonly IRepository<User> _usersRepository;

        public AccountsService(IRepository<User> usersRepository)
        {
            _usersRepository = usersRepository;
        }
        
        public bool AuthenticateUser(string login, string password)
        {
            User user = _usersRepository.GetAll().FirstOrDefault(u => u.Login == login);

            if (user == null)
                return false;

            string hash = GetHashFromPassword(password, user.Salt);

            return user.Password.Equals(hash);
        }

        public string GetHashFromPassword(string password, int salt)
        {
            string hash;
            var data = Encoding.UTF8.GetBytes(password + salt);

            using (SHA512 shaM = new SHA512Managed())
            {
                hash = Encoding.UTF8.GetString(shaM.ComputeHash(data));
            }
            
            return hash;
        }

        public int ComputeSalt()
        {
            Random random = new Random();
            int salt = random.Next(1, 1000000);
            return salt;
        }

        public string GetUserName(HttpContext context)
        {
            try
            {
                string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
                HttpCookie authCookie = context.Request.Cookies[cookieName]; //Get the cookie by it's name
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                return ticket.Name;
            }
            catch (NullReferenceException)
            {
                return string.Empty;
            }
        }
    }
}
