﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Linguist.DataLayer.Model;
using Linguist.Services.Interfaces;
using System.Text;

namespace Linguist.Web.Controllers
{
    public class WordController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly IWordsService _wordsService;
        private readonly ICategoriesService _categoriesService;

        public WordController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService, ICategoriesService categoriesService)
        {
            _userService = userService;
            _wordsService = wordsService;
            _accountsService = accountsService;
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public ActionResult Add(int categoryId = 0)
        {
            ViewBag.CategoriesListItems = GetUserCategoriesAsSelectList(categoryId);

            return View();
        }

        [HttpPost]
        public ActionResult Add(int categoryId, string originalWord, string translation)
        {
            if (string.IsNullOrEmpty(originalWord))
            {
                var operationMessage = $"Пустое слово не может быть сохранено";
                return Redirect(Url.Action("MyWords", "Home", new { categoryId, message = operationMessage }));
            }
            
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);

            User user = _userService.GetUserByLogin(login);

            if (_wordsService.WordIsAlreadySaved(login, originalWord))
            {
                var word = _userService.GetUserWords(login).FirstOrDefault(w => w.OriginalWord.ToLower().Equals(originalWord.ToLower()));
                var categoriesOfWord = _wordsService.GetCategoriesOfWord(word.WordId);
                StringBuilder sb = new StringBuilder();
                foreach (var category in categoriesOfWord)
                {
                    sb.Append(category.CategoryName + ", ");
                }
                sb.Remove(sb.Length - 2, 2);
                var operationMessage = $"Слово {originalWord} не сохранено, так как такое слово уже есть в словарях: {sb}";
                return Redirect(Url.Action("MyWords", "Home", new { categoryId, message = operationMessage }));
            }

            Word _word = new Word
            {
                UserId = user.UserId,
                OriginalWord = originalWord,
                Translation = translation,
                DateAdded = DateTime.Now,
                RememberIndex = 0
            };

            if (_wordsService.AddWord(_word, categoryId))
            {
                var operationMessage = $"Слово {originalWord} сохранено";
                return Redirect(Url.Action("MyWords", "Home", new {categoryId, message = operationMessage}));
            }
            else
            {
                var operationMessage = $"Слово {originalWord} не удалось сохранить";
                return Redirect(Url.Action("MyWords", "Home", new { categoryId, message = operationMessage }));
            }
        }

        public ActionResult Remove(int wordId, string returnUrl)
        {
            Word word = _wordsService.GetWordById(wordId);

            if (_wordsService.RemoveWord(wordId))
            {
                var operationMessage = $"Слово {word.OriginalWord} удалено";
                return Redirect(Url.Action("MyWords", "Home", new { message = operationMessage }));
            }

            if (word != null)
            {
                var operationMessage = $"Слово {word.OriginalWord} не удалось удалить";
                return Redirect(Url.Action("MyWords", "Home", new {message = operationMessage}));
            }
            else
            {
                var operationMessage = "Указанное слово не найдено";
                return Redirect(Url.Action("MyWords", "Home", new { message = operationMessage }));
            }
        }

        public ActionResult Edit(int wordId, string returnUrl)
        {
            Word word = _wordsService.GetWordById(wordId);

            if (word != null)
            {
                return View(word);
            }

            var operationMessage = "Указанное слово не найдено";
            return Redirect(Url.Action("MyWords", "Home", new { message = operationMessage }));
        }

        [HttpPost]
        public ActionResult Edit(Word word)
        {
            if (word == null)
            {
                var operationMessage = "Ошибка редактирования слова";
                return Redirect(Url.Action("MyWords", "Home", new { message = operationMessage }));
            }

            if (_wordsService.EditWord(word))
            {
                var operationMessage = $"Слово {word.OriginalWord} сохранено";
                return Redirect(Url.Action("MyWords", "Home", new {message = operationMessage}));
            }
            else
            {
                var operationMessage = $"Ошибка сохранения слова {word.OriginalWord}";
                return Redirect(Url.Action("MyWords", "Home", new { message = operationMessage }));
            }
        }

        private IEnumerable<SelectListItem> GetUserCategoriesAsSelectList(int selectedCategoryId = 0)
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);
            return _userService.GetUserCategories(login).Select(i => new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.CategoryId.ToString(),
                Selected = i.CategoryId == selectedCategoryId
            }).ToList();
        }
    }
}