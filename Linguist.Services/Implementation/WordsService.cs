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

        private readonly IRepository<Category> _categoriesRepository;

        private readonly IRepository<CatWordRelation> _relationsRepository;

        private readonly IUsersService _usersService;

        public WordsService(IRepository<Word> wordsRepository, IRepository<Category> categoriesRepository, IRepository<CatWordRelation> relationsRepository, IUsersService usersService)
        {
            _wordsRepository = wordsRepository;
            _categoriesRepository = categoriesRepository;
            _relationsRepository = relationsRepository;
            _usersService = usersService;
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

        public bool RemoveWord(int wordId)
        {
            var relationIds = _relationsRepository.GetAll().Where(r => r.WordId == wordId).Select(r => r.CatWordRelationId).ToList();
            foreach (var relationId in relationIds)
            {
                _relationsRepository.Remove(relationId);
            }

            if(_wordsRepository.Remove(wordId) > 0)
                return true;
            return false;
        }

        public IEnumerable<Word> GetWordsByCategory(int categoryId)
        {
            var wordsIds = _relationsRepository.GetAll().Where(r => r.CategoryId == categoryId).Select(r => r.WordId);
            var words = _wordsRepository.GetAll().Where(w => wordsIds.Contains(w.WordId));
            return words;
        }

        public Word GetWordById(int wordId)
        {
            var word = _wordsRepository.GetAll().FirstOrDefault(w => w.WordId == wordId);
            return word;
        }

        public bool WordIsAlreadySaved(string login, string originalWord)
        {
            var userWords = _usersService.GetUserWords(login).Select(w => w.OriginalWord);
            return userWords.Contains(originalWord);
        }

        public IEnumerable<Category> GetCategoriesOfWord(int wordId)
        {
            var categoriesIds = _relationsRepository.GetAll()
                .Where(r => r.WordId == wordId)
                .Select(r => r.CategoryId)
                .ToList();

            return _categoriesRepository.GetAll().Where(c => categoriesIds.Contains(c.CategoryId)).ToList();
        }
    }
}
