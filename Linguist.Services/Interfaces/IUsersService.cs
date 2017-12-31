using Linguist.DataLayer.Model;
using System.Collections.Generic;

namespace Linguist.Services.Interfaces
{
    public interface IUsersService
    {
        bool AddUser(User user);

        bool RemoveUser(User user);

        bool EditUser(User user);

        IEnumerable<Word> GetUserWords(string login);

        IEnumerable<Category> GetUserCategories(string login);
    }
}
