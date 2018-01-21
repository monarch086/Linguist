﻿using System;
using System.Linq;
using System.Web.Mvc;
using Linguist.Services.Interfaces;

namespace Linguist.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly IWordsService _wordsService;

        public TestController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService, ICategoriesService categoriesService)
        {
            _userService = userService;
            _wordsService = wordsService;
            _accountsService = accountsService;
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult AllWords()
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);
            var words = _userService.GetUserWords(login).ToList();

            var rnd = new Random();

            var wordsFrom0To3 = words
                .Where(w => w.RememberIndex < 4)
                .OrderBy(item => rnd.Next())
                .Take(20).ToList();

            var wordsFrom4To7 = words
                .Where(w => w.RememberIndex >= 4 && w.RememberIndex < 8)
                .OrderBy(item => rnd.Next())
                .Take(10).ToList();

            var wordsFrom8To9 = words
                .Where(w => w.RememberIndex >= 8)
                .OrderBy(item => rnd.Next())
                .Take(5).ToList();

            words.Clear();
            words.AddRange(wordsFrom0To3);
            words.AddRange(wordsFrom4To7);
            words.AddRange(wordsFrom8To9);

            return View("~/Views/Test/Test.cshtml", words);
        }

        public void SaveTestResults(int[] rightWords, int[] wrongWords)
        {
            _wordsService.IncreaseRememberIndex(rightWords);
            _wordsService.DecreaseRememberIndex(wrongWords);
        }
    }
}