﻿using System;
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
    public class BasketController : Controller
    {
        private ICRUDService<ShopingCart> _service;
        public BasketController(ICRUDService<ShopingCart> service)
        {
            this._service = service;
        }

        GetId getUserIdbyName = new GetId();
       
        public IActionResult Index()
        {
            var model = new BasketViewModel();
            Guid userId = getUserIdbyName.GetIdUser(HttpContext.User.Identity.Name);
            model.ShopingCarts = _service.Get(userId);
            model.UserName = HttpContext.User.Identity.Name;
            return View("~/Views/Basket/Index.cshtml", model);
        }

        public JsonResult DeleteFromBasket(Guid id)
        {
            var result = _service.Delete(id);
            if (result) return new JsonResult(new { message = "Success deleted item from basket." });

            return new JsonRedirect("ShopingCart isn't deleted.");
        }

        public JsonResult AddToBasket(Guid id, int quantity)
        {
            if (id != Guid.Empty)
            {
                var succes = false;
                Guid userId = getUserIdbyName.GetIdUser(HttpContext.User.Identity.Name);
                var exisingShopingCart = _service.Get(userId).FirstOrDefault(f => f.ProductId == id);                       
                if (exisingShopingCart == null)
                {
                    Basket basket; 
                    using (StoreDBContext db = new StoreDBContext())
                    {
                         basket = db.Baskets.FirstOrDefault(b => b.UserId == (db.Users.FirstOrDefault(f => f.UserId == userId).UserId));
                    }

                    var newModel = new ShopingCart() { 
                        Quantity = quantity,
                        ProductId = id, 
                        BasketId = basket.BasketId };
                     succes = _service.Create(newModel);
                }
                else
                {
                     exisingShopingCart.Quantity += quantity;
                     succes = _service.Update(exisingShopingCart);
                }
                    
                if (!succes) return new JsonResult(new { error = "Error while adding product!" });
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
