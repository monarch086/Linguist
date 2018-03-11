using System.Web.Mvc;
using System.Web.Routing;
using Linguist.Services.Interfaces;
using Linguist.Web.Models;

namespace Linguist.Web.Controllers
{
    public class OptionsController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly IWordsService _wordsService;
        private readonly IMailService _mailService;

        public OptionsController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService, IMailService mailService)
        {
            _accountsService = accountsService;
            _userService = userService;
            _wordsService = wordsService;
            _mailService = mailService;
        }

        public ActionResult Main()
        {
            return View();
        }

        public void ChangeAllQuotesToStars()
        {
            var context = System.Web.HttpContext.Current;
            var login = _accountsService.GetUserName(context);

            var words = _userService.GetUserWords(login);

            foreach (var word in words)
            {
                word.OriginalWord = word.OriginalWord.Replace("'","*");
                word.OriginalWord = word.OriginalWord.Replace("**", "*");
                _wordsService.EditWord(word);
            }
        }

        public void ShiftAllStarsLeft()
        {
            var context = System.Web.HttpContext.Current;
            var login = _accountsService.GetUserName(context);

            var words = _userService.GetUserWords(login);

            foreach (var word in words)
            {
                for (int i = 0; i < word.OriginalWord.Length; i++)
                {
                    if (word.OriginalWord[i] == '*')
                    {
                        word.OriginalWord = word.OriginalWord.Remove(i, 1);
                        word.OriginalWord = word.OriginalWord.Insert(i - 1, "*");
                    }
                }

                _wordsService.EditWord(word);
            }
        }

        public bool SetNewPassword(string oldPassword, string newPassword)
        {
            var context = System.Web.HttpContext.Current;

            var login = _accountsService.GetUserName(context);

            if (string.IsNullOrEmpty(login))
                return false;

            if (_accountsService.AuthenticateUser(login, oldPassword))
            {
                return _accountsService.SetPassword(login, newPassword);
            }

            return false;
        }

        public void SendResetMail(string login)
        {
            if (_userService.DoesLoginExist(login))
            {
                var restoreCode = _accountsService.GenerateRestoreCode(login);

                var subject = "Linguist: Reseting password";
                
                string link = Url.Action("ResetPassword", "Options",
                    new RouteValueDictionary(new { restoreCode }),
                    HttpContext.Request.Url.Scheme);

                var text = $"To reset password on Linguist site for login {login} click on {link}";

                _mailService.SendMail(login, text, subject);
            }
        }

        public ActionResult ResetPassword(string restoreCode)
        {
            var login = _userService.GetUserByRestoreCode(restoreCode).Login;
            return View(new ResetViewModel{ Login = login ?? ""});
        }

        [HttpPost]
        public string ResetPassword(ResetViewModel model)
        {
            var user = _userService.GetUserByLogin(model.Login);

            if (_accountsService.SetPassword(model.Login, model.Password))
            {
                user.RestoreCode = null;
                _userService.EditUser(user);
                return "Password changed";
            }

            return "Error while changing password";
        }
    }
}