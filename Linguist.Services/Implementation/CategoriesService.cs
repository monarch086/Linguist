using System.Linq;
using Linguist.DataLayer.Model;
using Linguist.DataLayer.Repositories;
using Linguist.Services.Interfaces;

namespace Linguist.Services.Implementation
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Word> _wordsRepository;

        private readonly IRepository<Category> _categoriesRepository;

        private readonly IRepository<CatWordRelation> _relationsRepository;

        public CategoriesService(IRepository<Word> wordsRepository, IRepository<Category> categoriesRepository, IRepository<CatWordRelation> relationsRepository)
        {
            _wordsRepository = wordsRepository;
            _categoriesRepository = categoriesRepository;
            _relationsRepository = relationsRepository;
        }

        public bool AddCategory(Category category)
        {
            if (_categoriesRepository.Add(category) > 0)
                return true;
            return false;
        }

        public bool EditCategory(Category category)
        {
            if (_categoriesRepository.Edit(category) > 0)
                return true;
            return false;
        }

        public bool RemoveCategory(Category category)
        {
            var relationIds = _relationsRepository.GetAll().Where(r => r.CategoryId == category.CategoryId).Select(r => r.CatWordRelationId);
            foreach (var relationId in relationIds)
            {
                _relationsRepository.Remove(relationId);
            }

            if (_wordsRepository.Remove(category.CategoryId) > 0)
                return true;
            return false;
        }
    }
}
