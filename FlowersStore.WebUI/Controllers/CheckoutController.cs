using FlowersStore.WebUI.Contracts;
using FlowersStore.Core.Services;
using FlowersStore.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.WebUI.Controllers
{
    [Authorize]
    [Authorize(Policy = "User")]
    public class CheckoutController : Controller
    {
        private readonly IShopingCartCRUDService<ShopingCart> _shopingCartservice;
        private readonly IUserService _userService;

        public CheckoutController(IShopingCartCRUDService<ShopingCart> shopingCartservice, IUserService userService)
        {
            this._shopingCartservice = shopingCartservice;
            this._userService = userService;
        }

        public IActionResult Index()
        {
            var checkoutModel = new CheckoutViewModel();

            var user = _userService.GetUser(HttpContext.User.Identity.Name);

            checkoutModel.ShopingCarts = _shopingCartservice.Get(user.Id);

            checkoutModel.UserName = user.Name;
            
            checkoutModel.Phone = user.PhoneNumber;
            
            checkoutModel.Email = user.Email;
            
            return View("~/Views/Checkout/Index.cshtml", checkoutModel);
        }
    }
}

