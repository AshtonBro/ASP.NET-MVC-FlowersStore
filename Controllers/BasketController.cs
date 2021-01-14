using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowersStore.Data;
using FlowersStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            var model = new BasketViewModel();
            using (StoreDBContext db = new StoreDBContext())
            {
                model.UserName = db.Users.FirstOrDefault(f => f.Name == "User1").Name;

                var user = db.Users.FirstOrDefault(f => f.Name == "User1");
                var basket = db.Baskets.FirstOrDefault(f => f.UserId == user.UserId);
               // model.ShopingCarts = basket.ShopingCarts;
                model.ShopingCarts = db.ShopingCarts.Where(f => f.Basket.BasketId == basket.BasketId).ToArray();

                foreach (var cart in model.ShopingCarts)
                {
                    cart.Product = db.Products.FirstOrDefault(f => f.ProductId == cart.ProductId);
                    cart.Product.Category = db.Categories.FirstOrDefault(f => f.CategoryId == cart.Product.CategoryId);
                }

            }

                return View("~/Views/Basket/Index.cshtml", model);
        }
    }
}
