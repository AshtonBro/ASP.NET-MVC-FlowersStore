using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.ViewModels
{
    public class LoginViewModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
