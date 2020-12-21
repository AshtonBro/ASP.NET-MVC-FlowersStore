using FlowersStore.Data;
using FlowersStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

            }

            return View(model);
        }
    }
}
