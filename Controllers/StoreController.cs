using FlowersStore.Data;
using FlowersStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            var model = new StoreViewModel();
            using (StoreDBContext db = new StoreDBContext())
            {
                model.Products = db.Products;
            }

            return View("~/Views/Store/Store.cshtml", model);
        }
    }
}
