using System;
using System.Linq;
using FlowersStore.WebUI.Contracts;
using FlowersStore.Core.Services;
using FlowersStore.WebUI.Helpers;
using FlowersStore.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.WebUI.Controllers
{
    [Authorize]
    [Authorize(Policy = "User")]
    public class BasketController : Controller
    {
        private readonly IShopingCartCRUDService<ShopingCart> _shopingCartService;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        private HttpContext _httpContext;

        public BasketController(
            HttpContext httpContext,
            IShopingCartCRUDService<ShopingCart> shopingCartservice,
            IBasketService basketService,
            IUserService userService)
        {
            _httpContext = httpContext;
            _shopingCartService = shopingCartservice;
            _basketService = basketService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var backetModel = _basketService.GetBasket(_httpContext.User.Identity.Name);

            //var user = _userService.GetUser(_httpContext.User.Identity.Name);

            //var shopingCarts = _shopingCartService.Get(user.Id);

            var model = new BasketViewModel
            {
                Name = backetModel.Name,
                UserName = backetModel.UserName,
               // ShopingCarts = backetModel.ShopingCarts // need map
            };

            return View("~/Views/Basket/Index.cshtml", model);
        }

        public JsonResult DeleteFromBasket(Guid id)
        {
            var result = _shopingCartService.Delete(id);

            if (result) return new JsonResult(new { message = "Success deleted item from basket." });

            return new JsonRedirect("ShopingCart isn't deleted.");
        }

        public JsonResult AddToBasket(Guid id, int quantity)
        {
            if (id != Guid.Empty)
            {
                var success = false;

                var userId = _userService.GetUser(HttpContext.User.Identity.Name).Id;

                var exisingShopingCart = _shopingCartService.Get(userId).FirstOrDefault(f => f.ProductId == id);

                if (exisingShopingCart == null)
                {
                    var basket = _basketService.GetBasket(userId);

                    var newModel = new ShopingCart()
                    {
                        Quantity = quantity,
                        ProductId = id,
                        BasketId = basket.BasketId
                    };

                    success = _shopingCartService.Create(newModel);
                }
                else
                {
                    exisingShopingCart.Quantity += quantity;

                    success = _shopingCartService.Update(exisingShopingCart);
                }

                if (!success) return new JsonResult(new { error = "Error while adding product!" });

                return new JsonResult(new { message = "Thank you! Item added to basket." });
            }
            return new JsonResult(new { error = "Error while adding product!" });
        }

        public JsonResult CleanBasket(string userName)
        {
            if (String.IsNullOrEmpty(userName)) new JsonResult(new { error = "User name is empty or null" });

            var user = _userService.GetUser(userName);

            if (user.Id == Guid.Empty) new JsonResult(new { error = "UserId is null" });

            _shopingCartService.DeleteAll(user.Id);

            return new JsonResult(new { message = "You successfully deleted all items." });
        }

        public JsonResult ChangeQuantity(Guid id, int quantity)
        {
            if (id != Guid.Empty)
            {
                var exisingShopingCart = _shopingCartService.GetById(id);

                if (exisingShopingCart != null)
                {
                    exisingShopingCart.Quantity = quantity;

                    bool success = _shopingCartService.Update(exisingShopingCart);

                    if (!success) return new JsonResult(new { error = "Error while changing quantity!" });

                    return new JsonResult(new { message = "You changed quantity." });
                }
                return new JsonResult(new { error = "Shopping Cart is empty!" });
            }
            return new JsonResult(new { error = "Error while changing quantity!" });
        }
    }
}

