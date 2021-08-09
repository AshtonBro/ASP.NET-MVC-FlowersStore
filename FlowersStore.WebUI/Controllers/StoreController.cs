using FlowersStore.Core.Services;
using FlowersStore.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.WebUI.Controllers
{
    [Authorize]
    [Authorize(Policy = "User")]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public IActionResult Index(StoreViewModel model)
        {
           // model.Products = _storeService.GetProducts();

            model.UserName = HttpContext.User.Identity.Name;

            return View(model);
        }
    }
}
