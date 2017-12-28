using Linguist.DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Linguist.Services
{
    public class UserService
    {
        public UserService()
        {
            
        }

        public bool AddUser(User user)
        {
            return true;
        }

        public bool RemoveUser(User user)
        {
            return true;
        }

        public IEnumerable<Word> GetUserWords(User user)
        {
            return null;
        }

        public IEnumerable<Category> GetUserCategories(User user)
        {
            return null;
        }
    }
}
