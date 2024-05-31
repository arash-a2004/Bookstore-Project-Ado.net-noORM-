using BookStore.Entities;
using BookStore.Services;
using BookStore.web.Filters;
using BookStore.web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.web.Controllers
{
    public class AccountController : Controller
    {
        private IAuthentication _authenticationServices;
        public AccountController(IAuthentication authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            if (_authenticationServices.checkUserExist(model.userName, model.password))
            {
                var user = _authenticationServices.checkUser(model.userName, model.password);
                if(user != null)
                {
                    var role = _authenticationServices.GetRole(user.RoleId);
                    HttpContext.Session.SetString("userName",user.UserName);
                    HttpContext.Session.SetString("role", role.Name);
                    HttpContext.Session.SetInt32("userId",user.Id);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(AuthenticatedUser model)
        {
            var result = _authenticationServices.AddUser(model);
            if(result >= 1)
            {
                return RedirectToAction("Login");   
            }
            return View();
        }
    }
}
