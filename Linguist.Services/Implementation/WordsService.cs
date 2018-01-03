using System.Linq;
using Linguist.DataLayer.Model;
using Linguist.DataLayer.Repositories;
using Linguist.Services.Interfaces;

namespace Linguist.Services.Implementation
{
    public class WordsService : IWordsService
    {
        private readonly IRepository<Word> _wordsRepository;

        private readonly IRepository<CatWordRelation> _relationsRepository;

        public WordsService(IRepository<Word> wordsRepository, IRepository<CatWordRelation> relationsRepository)
        {
            _wordsRepository = wordsRepository;
            _relationsRepository = relationsRepository;
        }

        public bool AddWord(Word word, Category category)
        {
            if(_wordsRepository.Add(word) == 0)
                return false;
            //int? wordId = _wordsRepository.GetAll().FirstOrDefault(w => w.OriginalWord.Equals(word.OriginalWord))?.WordId;
            if (_relationsRepository.Add(new CatWordRelation
            {
                WordId = word.WordId,
                CategoryId = category.CategoryId
            }) == 0)
                return false;
            return true;
        }

        public bool EditWord(Word word)
        {
            return true;
        }

        public bool RemoveWord(Word word)
        {
            return true;
        }
    }
}
