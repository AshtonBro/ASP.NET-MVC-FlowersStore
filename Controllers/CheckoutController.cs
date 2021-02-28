using FlowersStore.Helpers;
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

        public JsonRedirect CheckoutFrom(BasketViewModel model)
        {
            if(model.ShopingCarts != null)
            {
                return new JsonRedirect(new Link(nameof(CheckoutController), nameof(CheckoutController.Index)));
            }
            return new JsonRedirect("BasketViewModel is null.");
        }
    }
}

