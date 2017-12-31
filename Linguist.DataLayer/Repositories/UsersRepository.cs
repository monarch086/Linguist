using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Linguist.DataLayer.Context;
using Linguist.DataLayer.Model;
using Linguist.DataLayer.UnitOfWork;

namespace Linguist.DataLayer.Repositories
{
    public class UsersRepository : IRepository<User>
    {
        private readonly LinguistContext _context;

        private readonly IUnitOfWork _unitOfWork;

        public UsersRepository(LinguistContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork.UnitOfWork(context);
        }

        public int Add(User entity)
        {
            _context.Users.Add(entity);
            return _unitOfWork.Save();
        }

        public int Edit(User entity)
        {
            _context.Users.AddOrUpdate(entity);
            return _unitOfWork.Save();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public int Remove(User entity)
        {
            _context.Users.Remove(entity);
            return _unitOfWork.Save();
        }
    }
}
