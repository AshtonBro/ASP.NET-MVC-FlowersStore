using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FlowersStore.ViewModels;

namespace FlowersStore.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OpenLogin(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            return View(model);
        }

       
    }
}
