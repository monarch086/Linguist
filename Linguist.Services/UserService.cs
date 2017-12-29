using Linguist.DataLayer.Model;
using Linguist.DataLayer.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Linguist.Services
{
    public class UserService
    {
        private readonly IRepository<User> _usersRepository;

        private readonly IRepository<Word> _wordsRepository;

        private readonly IRepository<Category> _categoryRepository;

        public UserService(IRepository<User> usersRepository, IRepository<Word> wordsRepository, IRepository<Category> categoryRepository)
        {
            _usersRepository = usersRepository;
            _wordsRepository = wordsRepository;
            _categoryRepository = categoryRepository;
        }

        public bool AuthenticateUser(string login, string password)
        {
            User user = _usersRepository.GetAll().FirstOrDefault(u => u.Login == login);

            if (user == null)
                return false;

            string hash;
            var data = Encoding.UTF8.GetBytes(password + user.Salt);

            using (SHA512 shaM = new SHA512Managed())
            {
                hash = shaM.ComputeHash(data).ToString();
            }

            return user.Password.Equals(hash);
        }

        public bool AddUser(User user)
        {
            return _usersRepository.Add(user) > 0;
        }

        public bool RemoveUser(User user)
        {
            return _usersRepository.Remove(user) > 0;
        }

        public bool EditUser(User user)
        {
            return _usersRepository.Edit(user) > 0;
        }

        public IEnumerable<Word> GetUserWords(User user)
        {
            return _wordsRepository.GetAll().Where(w => w.UserId == user.UserId).ToList();
        }

        public IEnumerable<Category> GetUserCategories(User user)
        {
            IEnumerable<int> categoryIds =
                _wordsRepository.GetAll().Where(w => w.UserId == user.UserId).Select(w => w.CaregoryId);

            return _categoryRepository.GetAll().Where(c => categoryIds.Contains(c.CategoryId));
        }
    }
}
