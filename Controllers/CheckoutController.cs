using FlowersStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.Controllers
{
    [Authorize]
    [Authorize(Policy = "User")]
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckoutFrom(BasketViewModel model)
        {
            return View();
        }
    }
}

