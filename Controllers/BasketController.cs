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
                model.UserName = db.Users.FirstOrDefault(f => f.Name == "User1").ToString();
                model.ShopingCarts = db.ShopingCarts.ToArray();
            }

            return View("~/Views/Basket/Index.cshtml", model);
        }
    }
}
