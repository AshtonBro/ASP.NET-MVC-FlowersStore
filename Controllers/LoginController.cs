using System;
using Microsoft.AspNetCore.Mvc;
using FlowersStore.ViewModels;
using System.Net;

namespace FlowersStore.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
            }
            dynamic obj = new System.Dynamic.ExpandoObject();
            obj.error = "Error from Login Controller";
            return new JsonResult(obj, "error");
        }

       
    }
}
