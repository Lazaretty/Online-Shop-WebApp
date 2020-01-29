using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using OnlineShop_WebApp.Models; // пространство имен моделей RegisterModel и LoginModel
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using static OnlineShop_WebApp.Repos.DbContext;
using BCrypt;
using Microsoft.AspNetCore.Hosting;

namespace OnlineShop_WebApp.Controllers
{
    public class AccountController : Controller
    {
        IprContext repo;

        public AccountController(IprContext r)
        {
            repo = r;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login (Login model)
        {
            UserM user = repo.GetUser(model);

            if (repo.Login(model))
            {
                Authenticate(user);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Некорректные логин и(или) пароль"); // не понимаю, как вывусти эти ошибки в View
            
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserM model)
        {
           // if (ModelState.IsValid)
            {
                if (!repo.IsRegisted(model.Email))
                {
                    repo.AddUser(model);
                    Authenticate(model);
                    return RedirectToAction("Index", "Home");
                }
                //else
                //    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private void Authenticate(UserM model)
        {
            HttpContext.Session.SetString("Username", model.FIO);
            HttpContext.Session.SetString("UserRole", model.Role.ToString());
            HttpContext.Session.SetString("UserId", model.Id.ToString());
        }
    }

}





































