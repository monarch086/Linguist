using System;
using System.Linq;
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

        public ActionResult Edit(int categoryId)
        {
            var category = _categoriesService.GetCategories().FirstOrDefault(c => c.CategoryId == categoryId);

            if (category != null)
            {
                return View(category);
            }

            return Redirect(Url.Action("MyWords", "Home", new { categoryId }));
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (category == null)
            {
                var operationMessage = "Словарь не найден";
                return Redirect(Url.Action("MyWords", "Home", new { message = operationMessage }));
            }

            if (_categoriesService.EditCategory(category))
            {
                var operationMessage = $"Изменения словаря {category.CategoryName} сохранены";
                return Redirect(Url.Action("MyWords", "Home", new { categoryId = category.CategoryId, message = operationMessage }));
            }

            var _operationMessage = $"Ошибка сохранения изменений словаря {category.CategoryName}";
            return Redirect(Url.Action("MyWords", "Home", new { categoryId = category.CategoryId, message = _operationMessage }));
        }

        public ActionResult Delete(int categoryId)
        {
            var category = _categoriesService.GetCategories().FirstOrDefault(c => c.CategoryId == categoryId);

            if (category == null)
            {
                var operationMessage = "Словарь не найден";
                return Redirect(Url.Action("MyWords", "Home", new { message = operationMessage }));
            }

            if (_categoriesService.RemoveCategory(category))
            {
                var operationMessage = $"Словарь {category.CategoryName} удалён";
                return Redirect(Url.Action("MyWords", "Home", new { message = operationMessage }));
            }

            var _operationMessage = $"Ошибка удаления словаря {category.CategoryName}";
            return Redirect(Url.Action("MyWords", "Home", new { message = _operationMessage }));
        }

        public PartialViewResult CategoryMenu(int categoryId = 0, bool mobile = false, bool training = false)
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

            if (training)
                return PartialView("~/Views/Training/CategoryTraining.cshtml", model);

            return PartialView(model);
        }
    }
}