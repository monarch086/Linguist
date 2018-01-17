using System.Collections.Generic;
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

        public IEnumerable<Category> GetCategories()
        {
            return _categoriesRepository.GetAll();
        }

        public IEnumerable<int> GetCategoriesIdsByWordId(int wordId)
        {
            var categoriesIds = _relationsRepository.GetAll().Where(r => r.WordId == wordId).Select(r => r.CategoryId);

            return categoriesIds;
        }

        public IEnumerable<Category> GetCategoriesByWordId(int wordId)
        {
            var categoriesIds = GetCategoriesIdsByWordId(wordId);

            var categories = _categoriesRepository.GetAll().Where(c => categoriesIds.Contains(c.CategoryId)).ToList();

            return categories;
        }

        public void UpdateWordCategories(int wordId, int[] categoryIds)
        {
            if (categoryIds == null)
            {
                var relationIds = _relationsRepository.GetAll()
                    .Where(r => r.WordId == wordId)
                    .Select(r => r.CatWordRelationId);
                foreach (var relationId in relationIds)
                {
                    _relationsRepository.Remove(relationId);
                }

                return;
            }

            var currentCategories = GetCategoriesIdsByWordId(wordId);

            var addedCategories = categoryIds.Where(cid => !currentCategories.Contains(cid));
            var deletedCategories = currentCategories.Where(cid => !categoryIds.Contains(cid));

            foreach (var categoryId in addedCategories)
            {
                _relationsRepository.Add(new CatWordRelation { WordId = wordId, CategoryId = categoryId });
            }

            var relationIdsToRemove = _relationsRepository.GetAll()
                .Where(r => r.WordId == wordId && deletedCategories.Contains(r.CategoryId))
                .Select(r => r.CatWordRelationId);
            foreach (var relationId in relationIdsToRemove)
            {
                _relationsRepository.Remove(relationId);
            }
        }
    }
}
