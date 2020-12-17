using System;
using Microsoft.AspNetCore.Mvc;
using FlowersStore.ViewModels;

namespace FlowersStore.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            return View(model);
        }

       
    }
}
