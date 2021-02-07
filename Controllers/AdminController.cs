using FlowersStore.Data;
using FlowersStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlowersStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        [Authorize(Policy = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
