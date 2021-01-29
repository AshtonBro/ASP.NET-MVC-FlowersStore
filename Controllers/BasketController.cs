using System;
using System.Linq;
using FlowersStore.Data;
using FlowersStore.Helpers;
using FlowersStore.Models;
using FlowersStore.Services;
using FlowersStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowersStore.Controllers
{
    public class BasketController : Controller
    {
        private ICRUDService<ShopingCart> _service;
        public BasketController(ICRUDService<ShopingCart> service)
        {
            this._service = service;
        }

        public IActionResult Index()
        {
            var model = new BasketViewModel();
            Guid userId = Guid.Parse("63115EDA-142D-40CC-8C39-9CF543D02354");
            model.ShopingCarts = _service.Get(userId);
            return View("~/Views/Basket/Index.cshtml", model);
        }

        public JsonResult DeleteFromBasket(Guid id)
        {
            var result = _service.Delete(id);
            if (result) return new JsonResult(new { message = "Success deleted item from basket." });

            return new JsonRedirect("Shoping cart not deleted.");
        }

        public IActionResult Checkout(BasketViewModel model)
        {
            return View();
        }
    }
}
