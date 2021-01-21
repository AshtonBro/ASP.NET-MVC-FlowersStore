using System;
using System.Linq;
using FlowersStore.Data;
using FlowersStore.Helpers;
using FlowersStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
               
                model.ShopingCarts = db.ShopingCarts.Include(f => f.Product.Category).Where(f => f.Basket.BasketId == basket.BasketId).ToArray();
            }
                return View("~/Views/Basket/Index.cshtml", model);
        }

        public JsonResult DeleteFromBasket(Guid id)
        {
            using (StoreDBContext db = new StoreDBContext())
            {
                var currentCart = db.ShopingCarts.FirstOrDefault(f => f.CartId == id);
                if (currentCart != null)
                {
                    db.ShopingCarts.Remove(currentCart);
                }
                else
                {
                    return new JsonRedirect("Cart is not found!");
                }
                db.SaveChanges();
            }
                return new JsonResult(new { message = "Success deleted item from basket." });
           
        }
    }
}
