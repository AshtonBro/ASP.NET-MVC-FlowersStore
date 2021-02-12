using System;
using System.Linq;
using FlowersStore.Data;
using FlowersStore.Helpers;
using FlowersStore.Models;
using FlowersStore.Services;
using FlowersStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.Controllers
{
    [Authorize]
    [Authorize(Policy = "User")]
    public class BasketController : Controller
    {
        private readonly IShopingCartCRUDService<ShopingCart> _shopingCartservice;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;

        public BasketController(IShopingCartCRUDService<ShopingCart> shopingCartservice, IBasketService basketService, IUserService userService)
        {
            this._shopingCartservice = shopingCartservice;
            this._basketService = basketService;
            this._userService = userService;
        }

        public IActionResult Index()
        {
            var model = new BasketViewModel();
            var userId = _userService.GetUser(HttpContext.User.Identity.Name);
            model.ShopingCarts = _shopingCartservice.Get(userId);
            model.UserName = HttpContext.User.Identity.Name;
            return View("~/Views/Basket/Index.cshtml", model);
        }

        public JsonResult DeleteFromBasket(Guid id)
        {
            var result = _shopingCartservice.Delete(id);
            if (result) return new JsonResult(new { message = "Success deleted item from basket." });

            return new JsonRedirect("ShopingCart isn't deleted.");
        }

        public JsonResult AddToBasket(Guid id, int quantity)
        {
            if (id != Guid.Empty)
            {
                var success = false;
                var userId = _userService.GetUser(HttpContext.User.Identity.Name);
                var exisingShopingCart = _shopingCartservice.Get(userId).FirstOrDefault(f => f.ProductId == id);                       
                if (exisingShopingCart == null)
                {
                    var basket = _basketService.GetBasket(userId);
                    var newModel = new ShopingCart() 
                    { 
                        Quantity = quantity,
                        ProductId = id, 
                        BasketId = basket.BasketId 
                    };
                     success = _shopingCartservice.Create(newModel);
                }
                else
                {
                     exisingShopingCart.Quantity += quantity;
                     success = _shopingCartservice.Update(exisingShopingCart);
                }
                    
                if (!success) return new JsonResult(new { error = "Error while adding product!" });
                return new JsonResult(new { message = "Thank you! Item added to basket." });
            }
            return new JsonResult(new { error = "Error while adding product!" });
        }

        public IActionResult Checkout(BasketViewModel model)
        {
            return View();
        }
    }
}

