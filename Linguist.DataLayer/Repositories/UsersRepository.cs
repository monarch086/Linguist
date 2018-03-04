using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Linguist.DataLayer.Context;
using Linguist.DataLayer.Model;

namespace Linguist.DataLayer.Repositories
{
    public class UsersRepository : IRepository<User>
    {
        private readonly LinguistContext _context;

        public UsersRepository(LinguistContext context)
        {
            _context = context;
        }

        public int Add(User entity)
        {
            _context.Users.Add(entity);
            return _context.Save();
        }

        public int Edit(User entity)
        {
            _context.Users.AddOrUpdate(entity);
            return _context.Save();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public int Remove(int entityId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == entityId);
            if (user != null)
                _context.Users.Remove(user);
            return _context.Save();
        }
    }
}
