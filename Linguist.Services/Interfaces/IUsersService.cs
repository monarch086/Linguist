using Linguist.DataLayer.Model;
using System.Collections.Generic;

namespace Linguist.Services.Interfaces
{
    public interface IUsersService
    {
        bool AddUser(User user);

        bool RemoveUser(User user);

        bool EditUser(User user);

        IEnumerable<Word> GetUserWords(User user);

        IEnumerable<Category> GetUserCategories(User user);
    }
}
