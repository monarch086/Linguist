using System;
using Linguist.Services.Interfaces;
using System.Web.Mvc;
using Linguist.DataLayer.Model;
using Linguist.Web.Models;

namespace Linguist.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly ICategoriesService _categoriesService;

        public CategoryController(IAccountsService accountsService, IUsersService userService, ICategoriesService categoriesService)
        {
            _accountsService = accountsService;
            _userService = userService;
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Category category)
        {
            if (!string.IsNullOrEmpty(category.CategoryName))
            {
                var login = _accountsService.GetUserName(System.Web.HttpContext.Current);
                var user = _userService.GetUserByLogin(login);

                category.UserId = user.UserId;
                category.DateAdded = DateTime.Now;
                category.ParentCategoryId = 0;

                _categoriesService.AddCategory(category);

            }

            return Redirect(Url.Action("MyWords", "Home"));
        }

        public PartialViewResult CategoryMenu(int categoryId = 0, bool mobile = false)
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);
            var categories = _userService.GetUserCategories(login);

            CategoryViewModel model = new CategoryViewModel
            {
                Categories = categories,
                CurrentCategoryId = categoryId
            };

            if (mobile)
                return PartialView("~/Views/Category/CategoryMenu.Mobile.cshtml", model);

            return PartialView(model);
        }
    }
}