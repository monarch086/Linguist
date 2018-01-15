using Linguist.DataLayer.Context;
using Linguist.DataLayer.Model;
using Linguist.DataLayer.UnitOfWork;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Linguist.DataLayer.Repositories
{
    public class CategoriesRepository : IRepository<Category>
    {
        private readonly LinguistContext _context;

        private readonly IUnitOfWork _unitOfWork;

        public CategoriesRepository(LinguistContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork.UnitOfWork(context);
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
            return _context.Categories.ToList();
        }

        public int Remove(int entityId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == entityId);
            if (category != null)
                _context.Categories.Remove(category);
            return _unitOfWork.Save();
        }
    }
}
