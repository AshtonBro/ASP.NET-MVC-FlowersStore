﻿using System;
using System.Linq;
using FlowersStore.Helpers;
using FlowersStore.Models;
using FlowersStore.Services.ServicesInterfaces;
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
            var user = _userService.GetUser(HttpContext.User.Identity.Name);
            model.ShopingCarts = _shopingCartservice.Get(user.Id);
            model.UserName = user.UserName;
            model.Name = user.Name;
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
                var userId = _userService.GetUser(HttpContext.User.Identity.Name).Id;
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

        public JsonResult CleanBasket(string userName)
        {
            if (String.IsNullOrEmpty(userName)) new JsonResult(new { error = "User name is empty or null" });
            User user = _userService.GetUser(userName);
            if(user.Id == Guid.Empty) new JsonResult(new { error = "UserId is null" });
            _shopingCartservice.DeleteAll(user.Id);
            return new JsonResult(new { message = "You successfully deleted all items." });
        }

        public JsonResult ChangeQuantity(Guid id, int quantity)
        {
            if (id != Guid.Empty)
            {
                var exisingShopingCart = _shopingCartservice.GetById(id);
                if (exisingShopingCart != null)
                {
                    exisingShopingCart.Quantity = quantity;
                    bool success = _shopingCartservice.Update(exisingShopingCart);
                    if (!success) return new JsonResult(new { error = "Error while changing quantity!" });
                    return new JsonResult(new { message = "You changed quantity." });
                }
                return new JsonResult(new { error = "Shopping Cart is empty!" });
            }
            return new JsonResult(new { error = "Error while changing quantity!" });
        }
    }
}

