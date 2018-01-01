using System;
using Linguist.DataLayer.Model;
using Linguist.DataLayer.Repositories;
using Linguist.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Linguist.Services.Implementation
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<User> _usersRepository;

        private readonly IRepository<Word> _wordsRepository;

        private readonly IRepository<Category> _categoryRepository;

        public UsersService(IRepository<User> usersRepository, IRepository<Word> wordsRepository, IRepository<Category> categoryRepository)
        {
            _usersRepository = usersRepository;
            _wordsRepository = wordsRepository;
            _categoryRepository = categoryRepository;
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

        public User GetUserByLogin(string login)
        {
            return _usersRepository.GetAll().FirstOrDefault(u => u.Login.Equals(login));
        }

        public IEnumerable<Word> GetUserWords(string login)
        {
            if (login == null)
                throw new ArgumentNullException("Login should not be null");

            User user = _usersRepository.GetAll().FirstOrDefault(u => u.Login.Equals(login));

            return _wordsRepository.GetAll().Where(w => w.UserId == user.UserId).ToList();
        }

        public IEnumerable<Category> GetUserCategories(string login)
        {
            if (login == null)
                throw new ArgumentNullException("Login should not be null");

            User user = _usersRepository.GetAll().FirstOrDefault(u => u.Login.Equals(login));

            IEnumerable<int> categoryIds =
                _wordsRepository.GetAll().Where(w => w.UserId == user.UserId).Select(w => w.CategoryId);

            return _categoryRepository.GetAll().Where(c => categoryIds.Contains(c.CategoryId));
        }
    }
}
