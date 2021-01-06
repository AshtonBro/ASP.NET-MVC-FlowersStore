using FlowersStore.Data;
using FlowersStore.Models;
using FlowersStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FlowersStore.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            var model = new StoreViewModel();
            using (StoreDBContext db = new StoreDBContext())
            {
                model.Products = db.Products.ToArray();

                foreach (var product in model.Products)
                {
                    product.Category = db.Categories.FirstOrDefault(f => f.CategoryId == product.CategoryId);
                }

            }
            return View(model);
        }

        public IActionResult AddToBasket(Guid id, int quantity)
        {
            using (StoreDBContext db = new StoreDBContext())
            {
                var shoppingCart = new ShopingCart();

                shoppingCart.CartId = Guid.NewGuid();
                shoppingCart.DateCreated = DateTime.Now;
                shoppingCart.Product = db.Products.FirstOrDefault(f => f.ProductId == id);
                shoppingCart.Quantity = quantity;
                shoppingCart.Basket = db.Baskets.FirstOrDefault(b => b.UserId == (db.Users.FirstOrDefault(f => f.Name == "User1").UserId));

                db.ShopingCarts.Add(shoppingCart);
            }
            
            return JsonResult()
        }
    }
}
