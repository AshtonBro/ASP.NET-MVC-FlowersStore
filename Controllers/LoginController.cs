using System;
using Microsoft.AspNetCore.Mvc;
using FlowersStore.ViewModels;
using FlowersStore.Helpers;
using System.Linq;

namespace FlowersStore.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonRedirect Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                return new JsonRedirect( new Link(nameof(HomeController), nameof(HomeController.Index)));
            }
            var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();
            return new JsonRedirect(error.ErrorMessage);
        }

       
    }
}
