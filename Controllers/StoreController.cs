using FlowersStore.Data;
using FlowersStore.Models;
using FlowersStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                model.Products = db.Products.Include(f => f.Category).ToArray();
            }
            return View(model);
        }

        public IActionResult AddToBasket(Guid id, int quantity)
        {
            using (StoreDBContext db = new StoreDBContext())
            {
                var basket = db.Baskets.FirstOrDefault(b => b.UserId == (db.Users.FirstOrDefault(f => f.Name == "User1").UserId));
                var existingShopingCart = db.ShopingCarts.FirstOrDefault(f => f.BasketId == basket.BasketId && f.ProductId == id);

                if (existingShopingCart == null)
                {
                    ShopingCart shoppingCart = new ShopingCart
                    {
                        CartId = Guid.NewGuid(),
                        DateCreated = DateTime.Now,
                        Product = db.Products.FirstOrDefault(f => f.ProductId == id),
                        Quantity = quantity,
                        Basket = db.Baskets.FirstOrDefault(b => b.UserId == (db.Users.FirstOrDefault(f => f.Name == "User1").UserId))
                    };

                    db.ShopingCarts.Add(shoppingCart);
                } else
                {
                    existingShopingCart.Quantity += quantity;
                }

                db.SaveChanges();
            }
            return new JsonResult( new { message = "Success" });
        }
    }
}
