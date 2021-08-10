using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.WebUI.Aries.AdminPanel.Controllers
{
    [Authorize]
    [Authorize(Policy = ClaimPolicyMatch.ADMIN)]
    [Area("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
