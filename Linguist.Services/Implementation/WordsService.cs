﻿using System.Collections.Generic;
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

        public bool AddWord(Word word, int categoryId)
        {
            if(_wordsRepository.Add(word) == 0)
                return false;
            //int? wordId = _wordsRepository.GetAll().FirstOrDefault(w => w.OriginalWord.Equals(word.OriginalWord))?.WordId;
            if (_relationsRepository.Add(new CatWordRelation
            {
                WordId = word.WordId,
                CategoryId = categoryId
            }) == 0)
                return false;
            return true;
        }

        public bool EditWord(Word word)
        {
            if(_wordsRepository.Edit(word) > 0)
                return true;
            return false;
        }

        public bool RemoveWord(Word word)
        {
            var relationIds = _relationsRepository.GetAll().Where(r => r.WordId == word.WordId).Select(r => r.CatWordRelationId);
            foreach (var relationId in relationIds)
            {
                _relationsRepository.Remove(relationId);
            }

            if(_wordsRepository.Remove(word.WordId) > 0)
                return true;
            return false;
        }

        public IEnumerable<Word> GetWordsByCategory(int categoryId)
        {
            var wordsIds = _relationsRepository.GetAll().Where(r => r.CategoryId == categoryId).Select(r => r.WordId);
            var words = _wordsRepository.GetAll().Where(w => wordsIds.Contains(w.WordId));
            return words;
        }
    }
}
