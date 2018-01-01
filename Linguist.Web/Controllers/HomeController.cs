using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Linguist.Services.Interfaces;

namespace Linguist.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;

        public HomeController(IAccountsService accountsService, IUsersService userService)
        {
            _accountsService = accountsService;
            _userService = userService;
            
            //get instance of AccountController
            //_accountController = DependencyResolver.Current.GetService<AccountController>();
            //_accountController.ControllerContext = new ControllerContext(this.Request.RequestContext, _accountController);
        }

        public ActionResult MyWords()
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);

            if (string.IsNullOrEmpty(login))
                return Redirect(Url.Action("Start", "Account"));

            var words = _userService.GetUserWords(login);

            return View(words);
        }

        public ActionResult Train()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Statistics()
        {
            return View();
        }

        public ActionResult Options()
        {
            return View();
        }
    }
}