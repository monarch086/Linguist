using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Linguist.DataLayer.Context;
using Linguist.DataLayer.Model;
using Linguist.DataLayer.UnitOfWork;

namespace Linguist.DataLayer.Repositories
{
    public class CategoriesRepository : IRepository<Category>
    {
        private readonly LinguistContext _context;

        private readonly IUnitOfWork _unitOfWork;

        public CategoriesRepository(LinguistContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public int Add(Category entity)
        {
            _context.Categories.Add(entity);
            return _unitOfWork.Save();
        }

        public int Edit(Category entity)
        {
            _context.Categories.AddOrUpdate(entity);
            return _unitOfWork.Save();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }

        public int Remove(Category entity)
        {
            _context.Categories.Remove(entity);
            return _unitOfWork.Save();
        }
    }
}
